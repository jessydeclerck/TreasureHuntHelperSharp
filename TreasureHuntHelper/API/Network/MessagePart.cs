using System;
using Cookie.API.Utils.IO;

namespace Cookie.API.Network
{
    public class MessagePart
    {
        private byte[] _data;

        /// <summary>
        ///     Set to true when the message is whole
        /// </summary>
        public bool IsValid => Header.HasValue && Length.HasValue &&
                               Length == Data.Length;

        public int? Header { get; private set; }

        public int? MessageId => Header >> 2;

        public int? LengthBytesCount => Header & 0x3;

        public int? Length { get; private set; }

        public byte[] Data
        {
            get => _data;
            private set => _data = value;
        }

        /// <summary>
        ///     Build or continue building the message. Returns true if the resulted message is valid and ready to be parsed
        /// </summary>
        public bool Build(BigEndianReader reader)
        {
            if (IsValid)
                return true;

            if (reader.BytesAvailable >= 2 && !Header.HasValue)
                Header = reader.ReadShort();

            if (LengthBytesCount.HasValue &&
                reader.BytesAvailable >= LengthBytesCount && !Length.HasValue)
            {
                if (LengthBytesCount < 0 || LengthBytesCount > 3)
                    throw new Exception(
                        "Malformated Message Header, invalid bytes number to read message length (inferior to 0 or superior to 3)");

                Length = 0;

                // 3..0 or 2..0 or 1..0
                for (var i = LengthBytesCount.Value - 1; i >= 0; i--)
                    Length |= reader.ReadByte() << (i * 8);
            }

            // first case : no data read
            if (Data == null && Length.HasValue)
            {
                if (Length == 0)
                    Data = new byte[0];

                // enough bytes in the buffer to build a complete message
                if (reader.BytesAvailable >= Length)
                    Data = reader.ReadBytes(Length.Value);
                // not enough bytes, so we read what we can
                else if (Length > reader.BytesAvailable)
                    Data = reader.ReadBytes((int) reader.BytesAvailable);
            }
            //second case : the message was split and it missed some bytes
            if (Data == null || !Length.HasValue || !(Data.Length < Length)) return IsValid;
            var bytesToRead = 0;

            // still miss some bytes ...
            if (Data.Length + reader.BytesAvailable < Length)
                bytesToRead = (int) reader.BytesAvailable;

            // there is enough bytes in the buffer to complete the message :)
            else if (Data.Length + reader.BytesAvailable >= Length)
                bytesToRead = Length.Value - Data.Length;

            if (bytesToRead == 0) return IsValid;
            var oldLength = Data.Length;
            Array.Resize(ref _data, Data.Length + bytesToRead);
            Array.Copy(reader.ReadBytes(bytesToRead), 0, Data, oldLength, bytesToRead);

            return IsValid;
        }

        public bool Build(BigEndianReader reader, bool isClient)
        {
            bool isValid = this.IsValid;
            bool result;
            if (isValid)
            {
                result = true;
            }
            else
            {
                this.Data = reader.Data;
                bool flag = reader.BytesAvailable >= 2L && !this.Header.HasValue;
                if (flag)
                {
                    this.Header = new int?((int)reader.ReadShort());
                    if (isClient)
                    {
                        uint num = reader.ReadUInt();
                    }
                }
                bool arg_CF_0;
                if (this.LengthBytesCount.HasValue)
                {
                    long arg_AD_0 = reader.BytesAvailable;
                    int? num2 = this.LengthBytesCount;
                    if (arg_AD_0 >= (num2.HasValue ? new long?((long)num2.GetValueOrDefault()) : null))
                    {
                        arg_CF_0 = !this.Length.HasValue;
                        goto IL_CF;
                    }
                }
                arg_CF_0 = false;
                IL_CF:
                bool flag2 = arg_CF_0;
                if (flag2)
                {
                    bool flag3 = this.LengthBytesCount < 0 || this.LengthBytesCount > 3;
                    if (flag3)
                    {
                        throw new Exception("Malformated Message Header, invalid bytes number to read message length (inferior to 0 or superior to 3)");
                    }
                    this.Length = new int?(0);
                    for (int i = this.LengthBytesCount.Value - 1; i >= 0; i--)
                    {
                        this.Length |= (int)reader.ReadByte() << i * 8;
                    }
                }
                bool flag4 = this.Data == null && this.Length.HasValue;
                if (flag4)
                {
                    bool flag5 = this.Length == 0;
                    if (flag5)
                    {
                        this.Data = new byte[0];
                    }
                    long arg_234_0 = reader.BytesAvailable;
                    int? num2 = this.Length;
                    bool flag6 = arg_234_0 >= (num2.HasValue ? new long?((long)num2.GetValueOrDefault()) : null);
                    if (flag6)
                    {
                        this.Data = reader.ReadBytes(this.Length.Value);
                    }
                    else
                    {
                        num2 = this.Length;
                        bool flag7 = (num2.HasValue ? new long?((long)num2.GetValueOrDefault()) : null) > reader.BytesAvailable;
                        if (flag7)
                        {
                            this.Data = reader.ReadBytes((int)reader.BytesAvailable);
                        }
                    }
                }
                bool flag8 = this.Data != null && this.Length.HasValue && this.Data.Length < this.Length;
                if (flag8)
                {
                    int num3 = 0;
                    long arg_356_0 = (long)this.Data.Length + reader.BytesAvailable;
                    int? num2 = this.Length;
                    bool flag9 = arg_356_0 < (num2.HasValue ? new long?((long)num2.GetValueOrDefault()) : null);
                    if (flag9)
                    {
                        num3 = (int)reader.BytesAvailable;
                    }
                    else
                    {
                        long arg_3B5_0 = (long)this.Data.Length + reader.BytesAvailable;
                        num2 = this.Length;
                        bool flag10 = arg_3B5_0 >= (num2.HasValue ? new long?((long)num2.GetValueOrDefault()) : null);
                        if (flag10)
                        {
                            num3 = this.Length.Value - this.Data.Length;
                        }
                    }
                    bool flag11 = num3 != 0;
                    if (flag11)
                    {
                        int destinationIndex = this.Data.Length;
                        Array.Resize<byte>(ref this._data, this.Data.Length + num3);
                        Array.Copy(reader.ReadBytes(num3), 0, this.Data, destinationIndex, num3);
                    }
                }
                result = this.IsValid;
            }
            return result;
        }
    }
}