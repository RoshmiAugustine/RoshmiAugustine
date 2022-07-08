IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210526041012_updateCountryListOrder_Migration')
BEGIN
    UPDATE [info].[Country] set ListOrder = 1 where [Name] = 'United States of America' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 2 where [Name] = 'England' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 3 where [Name] = 'Afghanistan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 4 where [Name] = 'Albania' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 5 where [Name] = 'Algeria' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 6 where [Name] = 'American Samoa' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 7 where [Name] = 'Andorra' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 8 where [Name] = 'Angola' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 9 where [Name] = 'Anguilla' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 10 where [Name] = 'Antarctica' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 11 where [Name] = 'Antigua And Barbuda' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 12 where [Name] = 'Argentina' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 13 where [Name] = 'Armenia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 14 where [Name] = 'Aruba' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 15 where [Name] = 'Australia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 16 where [Name] = 'Austria' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 17 where [Name] = 'Azerbaijan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 18 where [Name] = 'Bahamas' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 19 where [Name] = 'Bahrain' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 20 where [Name] = 'Bangladesh' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 21 where [Name] = 'Barbados' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 22 where [Name] = 'Belarus' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 23 where [Name] = 'Belgium' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 24 where [Name] = 'Belize' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 25 where [Name] = 'Benin' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 26 where [Name] = 'Bermuda' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 27 where [Name] = 'Bhutan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 28 where [Name] = 'Bolivia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 29 where [Name] = 'Bosnia And Herzegovina' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 30 where [Name] = 'Botswana' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 31 where [Name] = 'Bouvet Island' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 32 where [Name] = 'Brazil' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 33 where [Name] = 'British Indian Ocean Territory' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 34 where [Name] = 'Brunei Darussalam' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 35 where [Name] = 'Bulgaria' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 36 where [Name] = 'Burkina Faso' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 37 where [Name] = 'Burundi' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 38 where [Name] = 'Cambodia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 39 where [Name] = 'Cameroon' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 40 where [Name] = 'Canada' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 41 where [Name] = 'Cape Verde' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 42 where [Name] = 'Cayman Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 43 where [Name] = 'Central African Republic' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 44 where [Name] = 'Chad' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 45 where [Name] = 'Chile' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 46 where [Name] = 'China' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 47 where [Name] = 'Christmas Island' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 48 where [Name] = 'Cocos (keeling) Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 49 where [Name] = 'Colombia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 50 where [Name] = 'Comoros' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 51 where [Name] = 'Congo' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 52 where [Name] = 'Congo, The Democratic Republic Of The' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 53 where [Name] = 'Cook Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 54 where [Name] = 'Costa Rica' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 55 where [Name] = 'Cote D''ivoire' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 56 where [Name] = 'Croatia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 57 where [Name] = 'Cuba' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 58 where [Name] = 'Cyprus' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 59 where [Name] = 'Czech Republic' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 60 where [Name] = 'Denmark' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 61 where [Name] = 'Djibouti' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 62 where [Name] = 'Dominica' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 63 where [Name] = 'Dominican Republic' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 64 where [Name] = 'Ecuador' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 65 where [Name] = 'Egypt' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 66 where [Name] = 'El Salvador' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 67 where [Name] = 'Equatorial Guinea' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 68 where [Name] = 'Eritrea' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 69 where [Name] = 'Estonia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 70 where [Name] = 'Ethiopia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 71 where [Name] = 'Falkland Islands (malvinas)' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 72 where [Name] = 'Faroe Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 73 where [Name] = 'Fiji' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 74 where [Name] = 'Finland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 75 where [Name] = 'France' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 76 where [Name] = 'French Guiana' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 77 where [Name] = 'French Polynesia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 78 where [Name] = 'French Southern Territories' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 79 where [Name] = 'Gabon' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 80 where [Name] = 'Gambia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 81 where [Name] = 'Georgia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 82 where [Name] = 'Germany' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 83 where [Name] = 'Ghana' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 84 where [Name] = 'Gibraltar' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 85 where [Name] = 'Greece' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 86 where [Name] = 'Greenland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 87 where [Name] = 'Grenada' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 88 where [Name] = 'Guadeloupe' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 89 where [Name] = 'Guam' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 90 where [Name] = 'Guatemala' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 91 where [Name] = 'Guinea' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 92 where [Name] = 'Guinea-bissau' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 93 where [Name] = 'Guyana' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 94 where [Name] = 'Haiti' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 95 where [Name] = 'Heard Island And Mcdonald Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 96 where [Name] = 'Holy See (vatican City State)' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 97 where [Name] = 'Honduras' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 98 where [Name] = 'Hong Kong' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 99 where [Name] = 'Hungary' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 100 where [Name] = 'Iceland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 101 where [Name] = 'India' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 102 where [Name] = 'Indonesia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 103 where [Name] = 'Iran, Islamic Republic Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 104 where [Name] = 'Iraq' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 105 where [Name] = 'Ireland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 106 where [Name] = 'Israel' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 107 where [Name] = 'Italy' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 108 where [Name] = 'Jamaica' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 109 where [Name] = 'Japan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 110 where [Name] = 'Jordan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 111 where [Name] = 'Kazakhstan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 112 where [Name] = 'Kenya' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 113 where [Name] = 'Kiribati' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 114 where [Name] = 'Korea, Democratic People''s Republic Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 115 where [Name] = 'Korea, Republic Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 116 where [Name] = 'Kuwait' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 117 where [Name] = 'Kyrgyzstan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 118 where [Name] = 'Lao People''s Democratic Republic' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 119 where [Name] = 'Latvia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 120 where [Name] = 'Lebanon' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 121 where [Name] = 'Lesotho' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 122 where [Name] = 'Liberia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 123 where [Name] = 'Libyan Arab Jamahiriya' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 124 where [Name] = 'Liechtenstein' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 125 where [Name] = 'Lithuania' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 126 where [Name] = 'Luxembourg' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 127 where [Name] = 'Macao' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 128 where [Name] = 'Macedonia, The Former Yugoslav Republic Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 129 where [Name] = 'Madagascar' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 130 where [Name] = 'Malawi' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 131 where [Name] = 'Malaysia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 132 where [Name] = 'Maldives' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 133 where [Name] = 'Mali' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 134 where [Name] = 'Malta' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 135 where [Name] = 'Marshall Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 136 where [Name] = 'Martinique' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 137 where [Name] = 'Mauritania' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 138 where [Name] = 'Mauritius' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 139 where [Name] = 'Mayotte' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 140 where [Name] = 'Mexico' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 141 where [Name] = 'Micronesia, Federated States Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 142 where [Name] = 'Moldova, Republic Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 143 where [Name] = 'Monaco' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 144 where [Name] = 'Mongolia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 145 where [Name] = 'Montserrat' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 146 where [Name] = 'Morocco' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 147 where [Name] = 'Mozambique' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 148 where [Name] = 'Myanmar' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 149 where [Name] = 'Namibia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 150 where [Name] = 'Nauru' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 151 where [Name] = 'Nepal' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 152 where [Name] = 'Netherlands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 153 where [Name] = 'Netherlands Antilles' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 154 where [Name] = 'New Caledonia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 155 where [Name] = 'New Zealand' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 156 where [Name] = 'Nicaragua' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 157 where [Name] = 'Niger' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 158 where [Name] = 'Nigeria' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 159 where [Name] = 'Niue' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 160 where [Name] = 'Norfolk Island' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 161 where [Name] = 'Northern Mariana Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 162 where [Name] = 'Norway' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 163 where [Name] = 'Oman' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 164 where [Name] = 'Pakistan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 165 where [Name] = 'Palau' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 166 where [Name] = 'Palestinian Territory, Occupied' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 167 where [Name] = 'Panama' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 168 where [Name] = 'Papua New Guinea' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 169 where [Name] = 'Paraguay' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 170 where [Name] = 'Peru' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 171 where [Name] = 'Philippines' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 172 where [Name] = 'Pitcairn' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 173 where [Name] = 'Poland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 174 where [Name] = 'Portugal' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 175 where [Name] = 'Puerto Rico' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 176 where [Name] = 'Qatar' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 177 where [Name] = 'Reunion' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 178 where [Name] = 'Romania' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 179 where [Name] = 'Russian Federation' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 180 where [Name] = 'Rwanda' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 181 where [Name] = 'Saint Helena' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 182 where [Name] = 'Saint Kitts And Nevis' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 183 where [Name] = 'Saint Lucia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 184 where [Name] = 'Saint Pierre And Miquelon' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 185 where [Name] = 'Saint Vincent And The Grenadines' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 186 where [Name] = 'Samoa' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 187 where [Name] = 'San Marino' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 188 where [Name] = 'Sao Tome And Principe' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 189 where [Name] = 'Saudi Arabia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 190 where [Name] = 'Senegal' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 191 where [Name] = 'Serbia And Montenegro' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 192 where [Name] = 'Seychelles' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 193 where [Name] = 'Sierra Leone' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 194 where [Name] = 'Singapore' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 195 where [Name] = 'Slovakia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 196 where [Name] = 'Slovenia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 197 where [Name] = 'Solomon Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 198 where [Name] = 'Somalia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 199 where [Name] = 'South Africa' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 200 where [Name] = 'South Georgia And The South Sandwich Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 201 where [Name] = 'Spain' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 202 where [Name] = 'Sri Lanka' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 203 where [Name] = 'Sudan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 204 where [Name] = 'Suriname' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 205 where [Name] = 'Svalbard And Jan Mayen' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 206 where [Name] = 'Swaziland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 207 where [Name] = 'Sweden' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 208 where [Name] = 'Switzerland' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 209 where [Name] = 'Syrian Arab Republic' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 210 where [Name] = 'Taiwan, Province Of China' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 211 where [Name] = 'Tajikistan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 212 where [Name] = 'Tanzania, United Republic Of' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 213 where [Name] = 'Thailand' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 214 where [Name] = 'Timor-leste' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 215 where [Name] = 'Togo' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 216 where [Name] = 'Tokelau' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 217 where [Name] = 'Tonga' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 218 where [Name] = 'Trinidad And Tobago' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 219 where [Name] = 'Tunisia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 220 where [Name] = 'Turkey' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 221 where [Name] = 'Turkmenistan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 222 where [Name] = 'Turks And Caicos Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 223 where [Name] = 'Tuvalu' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 224 where [Name] = 'Uganda' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 225 where [Name] = 'Ukraine' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 226 where [Name] = 'United Arab Emirates' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 227 where [Name] = 'United States Minor Outlying Islands' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 228 where [Name] = 'Uruguay' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 229 where [Name] = 'Uzbekistan' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 230 where [Name] = 'Vanuatu' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 231 where [Name] = 'Venezuela' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 232 where [Name] = 'Viet Nam' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 233 where [Name] = 'Virgin Islands, British' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 234 where [Name] = 'Virgin Islands, U.s.' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 235 where [Name] = 'Wallis And Futuna' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 236 where [Name] = 'Western Sahara' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 237 where [Name] = 'Yemen' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 238 where [Name] = 'Zambia' AND IsRemoved = 0
    UPDATE [info].[Country] set ListOrder = 239 where [Name] = 'Zimbabwe' AND IsRemoved = 0
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210526041012_updateCountryListOrder_Migration', N'3.1.4');
END;

GO

