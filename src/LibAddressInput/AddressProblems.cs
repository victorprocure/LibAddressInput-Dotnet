using System.Collections.Generic;
using System.Linq;

namespace LibAddressInput
{
    public sealed class AddressProblems
    {
        private Dictionary<AddressField, AddressProblemType> _problems = new Dictionary<AddressField, AddressProblemType>();

        private void Add(AddressField addressField, AddressProblemType addressProblemType)
            => _problems.Add(addressField, addressProblemType);

        public bool IsEmpty()
            => !_problems.Any();
        
        public override string ToString()
            => _problems.ToString();

        public void Clear()
            => _problems.Clear();

        public AddressProblemType? GetProblemType(AddressField addressField)
            => _problems.ContainsKey(addressField) ? _problems[addressField] : (AddressProblemType?)null;
        
        public Dictionary<AddressField, AddressProblemType> Problems => _problems;
    }
}