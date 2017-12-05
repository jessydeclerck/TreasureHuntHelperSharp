// Generated on 12/06/2016 11:35:52

using Cookie.API.Gamedata.D2o;
using System.Collections.Generic;

namespace Cookie.API.Datacenter
{
    [D2oClass("SpellVariants")]
    public class SpellVariant : IDataObject
    {
        public const string MODULE = "SpellVariants";

        public int Id;

        public uint BreedId;
      
        public List<uint> SpellIds;
    }
}