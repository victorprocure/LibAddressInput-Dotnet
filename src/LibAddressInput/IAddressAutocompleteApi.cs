using System;

namespace LibAddressInput
{
    public interface IAddressAutocompleteApi
    {
        bool IsConfiguredCorrectly { get; }

        void GetAutocompletePredictions(string query, Action<AddressAutocompletePrediction> callback);
    }
}