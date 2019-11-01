using System;
using System.Collections.Generic;

namespace LibAddressInput
{
    public static class AddressDataKey
    {
        private static Dictionary<string, AddressDataKeys> AddressKeyNameMap = new Dictionary<string, AddressDataKeys>();

        public static AddressDataKeys Get(string keyName)
        {
            var lc = keyName.ToLowerInvariant();
            return AddressKeyNameMap[lc];
        }

        static AddressDataKey()
        {
            foreach (AddressDataKeys field in Enum.GetValues(typeof(AddressDataKeys)))
            {
                AddressKeyNameMap.Add(field.ToString().ToLowerInvariant(), field);
            }
        }
    }
    public enum AddressDataKeys
    {
        Countries,
        Fmt,
        Id,
        IsoId,
        Key,
        Lang,
        Languages,
        LFmt,
        LocalityNameType,
        Require,
        StateNameType,
        SubLocalityNameType,
        SubKeys,
        SubLNames,
        SubMores,
        SubNames,
        WidthOverrides,
        XZip,
        ZipNameType,
    }
}