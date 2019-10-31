using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LibAddressInput.Utils.StringUtils;

namespace LibAddressInput
{
    public sealed class AddressData
    {
        public AddressData(Builder builder)
        {
            PostalCountry = builder.GetValue(AddressField.Country);
            AdministrativeArea = builder.GetValue(AddressField.AdminArea);
            Locality = builder.GetValue(AddressField.Locality);
            DependentLocality = builder.GetValue(AddressField.DependentLocality);
            PostalCode = builder.GetValue(AddressField.PostalCode);
            SortingCode = builder.GetValue(AddressField.SortingCode);
            Organization = builder.GetValue(AddressField.Organization);
            Recipient = builder.GetValue(AddressField.Recipient);
            AddressLines = builder.AddressLines;
            LanguageCode = builder.Language;
        }
        /// <summary>
        /// Gets the Common Locale Data Repository (CLDR) country code.
        /// </summary>
        /// <value>CLDR Country Code</value>
        public string? PostalCountry { get; }

        /// <summary>
        /// Gets the Address Lines
        /// </summary>
        /// <remarks>
        /// The most specific part of any address. They may be left empty if more detailed fields
        /// are used instead, or they may be used in addition to these if the more detailed fields do not
        /// fulfil requirements, or they may be used instead of more detailed fields to represent the street
        /// level part.
        /// </remarks>
        /// <value>Available address lines</value>
        public IEnumerable<string> AddressLines { get; } = new List<string>();

        /// <summary>
        /// Gets the top-level administrative subdivision of this country.
        /// </summary>
        /// <value>The administrative area</value>
        public string? AdministrativeArea { get; }

        /// <summary>
        /// Gets City/Town portion of an address
        /// </summary>
        /// <value>Referes to the City/Town portion of an address</value>
        public string? Locality { get; }

        /// <summary>
        /// Gets the locality or sublocality.
        /// </summary>
        /// <remarks>Used for neighbourhoods or suburbs</remarks>
        /// <value>Locality or sublocality</value>
        public string? DependentLocality { get; }

        /// <summary>
        /// Gets the postal code
        /// </summary>
        /// <value>Frequently alphanumeric</value>
        public string? PostalCode { get; }

        /// <summary>
        /// Gets the sorting code
        /// </summary>
        /// <remarks>
        /// This corresponds to the SortingCode sub-element of the xAL PostalServiceElements element. 
        /// Use is very country specific
        /// </remarks>
        /// <value>Sorting code of the xAL PostalServiceElements element</value>
        public string? SortingCode { get; }

        /// <summary>
        /// Gets the firm or organization.
        /// </summary>
        /// <remarks>
        /// This goes at a finer granularity than address lines in the address.
        /// </remarks>
        /// <value>Firm or organization</value>
        public string? Organization { get; }

        /// <summary>
        /// Gets the recipient.
        /// </summary>
        /// <remarks>This goes at a finer granularity than address lines in the address. Not present in xAL.</remarks>
        /// <value>The Recipient</value>
        public string? Recipient { get; }

        /// <summary>
        /// Gets the BCP-47 Language Code for the address
        /// </summary>
        /// <value>BCP-47 Language code, can be null</value>
        public string? LanguageCode { get; }

        private static IEnumerable<string> NormalizeAddressLines(IEnumerable<string> addressLines)
        {
            foreach (var line in addressLines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.Contains('\n'))
                {
                    foreach (var splitLine in line.Split('\n'))
                    {
                        var trimmed = TrimToNull(splitLine);
                        if (trimmed != null)
                            yield return trimmed;
                    }
                }
                else
                {
                    var trimmed = TrimToNull(line);
                    if (trimmed != null)
                        yield return trimmed;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (!(obj is AddressData addressData))
                return false;

            return (PostalCountry == null
                    ? addressData.PostalCountry == null
                    : PostalCountry.Equals(addressData.PostalCountry))
                && (!AddressLines.Any()
                    ? !addressData.AddressLines.Any()
                    : AddressLines.Equals(addressData.AddressLines))
                && (AdministrativeArea == null
                    ? addressData.AdministrativeArea == null
                    : AdministrativeArea.Equals(addressData.AdministrativeArea))
                && (Locality == null
                    ? addressData.Locality == null
                    : Locality.Equals(addressData.Locality))
                && (DependentLocality == null
                    ? addressData.DependentLocality == null
                    : DependentLocality.Equals(addressData.DependentLocality))
                && (PostalCode == null
                    ? addressData.PostalCode == null
                    : PostalCode.Equals(addressData.PostalCode))
                && (SortingCode == null
                    ? addressData.SortingCode == null
                    : SortingCode.Equals(addressData.SortingCode))
                && (Organization == null
                    ? addressData.Organization == null
                    : Organization.Equals(addressData.Organization))
                && (Recipient == null
                    ? addressData.Recipient == null
                    : Recipient.Equals(addressData.Recipient))
                && (LanguageCode == null
                    ? addressData.LanguageCode == null
                    : LanguageCode.Equals(addressData.LanguageCode));
        }

        public override int GetHashCode()
        {
            var result = 17;

            var fields = new[]{
                PostalCountry,
                AdministrativeArea,
                Locality,
                DependentLocality,
                PostalCode,
                SortingCode,
                Organization,
                Recipient,
                LanguageCode
            };

            foreach(var field in fields)
                result = 31 * result + (field == null ? 0 : field.GetHashCode());
            
            result = 31 * result + (!AddressLines.Any() ? 0 : AddressLines.GetHashCode());

            return result;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder($"(AddressData: PostalCountry=${PostalCountry}; ");
            stringBuilder.Append($"Language={LanguageCode}; ");
            foreach (var line in AddressLines)
            {
                stringBuilder.Append($"{line}; ");
            }
            stringBuilder.Append($"AdminArea={AdministrativeArea}; ");
            stringBuilder.Append($"Locality={Locality}; ");
            stringBuilder.Append($"DependentLocality={DependentLocality}; ");
            stringBuilder.Append($"PostalCode={PostalCode}; ");
            stringBuilder.Append($"SortingCode={SortingCode}; ");
            stringBuilder.Append($"Organization={Organization}; ");
            stringBuilder.Append($"Recipient={Recipient})");

            return stringBuilder.ToString();
        }

        private string? GetFieldValue(AddressField addressField)
        {
            switch (addressField)
            {
                case AddressField.AdminArea:
                    return AdministrativeArea;
                case AddressField.Country:
                    return PostalCountry;
                case AddressField.DependentLocality:
                    return DependentLocality;
                case AddressField.Locality:
                    return Locality;
                case AddressField.Organization:
                    return Organization;
                case AddressField.PostalCode:
                    return PostalCode;
                case AddressField.Recipient:
                    return Recipient;
                case AddressField.SortingCode:
                    return SortingCode;
                default:
                    throw new InvalidOperationException("Multi value fields not supported: " + addressField);
            }
        }

        public sealed class Builder
        {
            private readonly Dictionary<AddressField, string> _fields = new Dictionary<AddressField, string>();
            internal List<string> AddressLines { get; } = new List<string>();
            internal string? Language { get; private set; } = null;

            private static readonly IEnumerable<AddressField> _singleValueFields = new[]
            {
                AddressField.AdminArea,
                AddressField.Country,
                AddressField.DependentLocality,
                AddressField.Locality,
                AddressField.Organization,
                AddressField.PostalCode,
                AddressField.Recipient,
                AddressField.SortingCode
            };

            public Builder() { }

            internal string? GetValue(AddressField addressField)
            {
                if (!_fields.ContainsKey(addressField))
                    return null;

                return _fields[addressField];
            }

            public Builder Set(AddressData addressData)
            {
                _fields.Clear();

                foreach (var field in _singleValueFields)
                {
                    Set(field, addressData.GetFieldValue(field));
                }

                AddressLines.Clear();
                AddressLines.AddRange(addressData.AddressLines);
                SetLanguageCode(addressData.LanguageCode);

                return this;
            }

            public Builder SetLanguageCode(string? languageCode)
            {
                Language = languageCode;

                return this;
            }

            public Builder SetCountry(string country)
                => Set(AddressField.Country, CheckNotNull(country));

            public Builder SetAdminArea(string adminArea)
                => Set(AddressField.AdminArea, adminArea);

            public Builder SetLocality(string locality)
                => Set(AddressField.Locality, locality);

            public Builder SetDependentLocality(string dependentLocality)
                => Set(AddressField.DependentLocality, dependentLocality);

            public Builder SetPostalCode(string postalCode)
                => Set(AddressField.PostalCode, postalCode);

            public Builder SetSortingCode(string sortingCode)
                => Set(AddressField.SortingCode, sortingCode);

            public Builder SetOrganization(string organization)
                => Set(AddressField.Organization, organization);

            public Builder SetRecipient(string recipient)
                => Set(AddressField.Recipient, recipient);

            public Builder SetAddressLines(IEnumerable<string> lines)
            {
                AddressLines.Clear();
                AddressLines.AddRange(lines);

                return this;
            }

            public Builder AddAddressLines(string value)
            {
                AddressLines.Add(value);
                return this;
            }

            public Builder SetAddress(string value)
            {
                AddressLines.Clear();
                AddressLines.AddRange(NormalizeAddressLines(AddressLines));

                return this;
            }

            private Builder Set(AddressField addressField, string? value)
            {
                if (_singleValueFields.Contains(addressField))
                {
                    value = value?.Trim();
                    if (string.IsNullOrWhiteSpace(value))
                        value = null;

                    if (value == null)
                        _fields.Remove(addressField);
                    else
                        _fields.Add(addressField, value);
                }

                return this;
            }
        }
    }
}
