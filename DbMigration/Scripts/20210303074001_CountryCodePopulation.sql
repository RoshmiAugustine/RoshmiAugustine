IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303074001_CountryCodePopulation')
BEGIN
update info.Country set IsRemoved=1

INSERT INTO info.Country ( Abbrev, Name, Description, ListOrder, IsRemoved, UpdateDate, UpdateUserID, CountryCode) VALUES
( 'AF', 'AFGHANISTAN', 'Afghanistan', 1, 0, getdate(),1, 93),
( 'AL', 'ALBANIA', 'Albania', 2, 0,getdate(),1 ,355),
( 'DZ', 'ALGERIA', 'Algeria', 3, 0, getdate(),1, 213),
( 'AS', 'AMERICAN SAMOA', 'American Samoa', 4, 0,getdate(),1 ,1684),
( 'AD', 'ANDORRA', 'Andorra', 5, 0,getdate(),1 , 376),
( 'AO', 'ANGOLA', 'Angola', 6, 0,getdate(),1 , 244),
( 'AI', 'ANGUILLA', 'Anguilla', 7, 0,getdate(),1 ,1264),
( 'AQ', 'ANTARCTICA', 'Antarctica', 8, 0,getdate(),1 , 672),
( 'AG', 'ANTIGUA AND BARBUDA', 'Antigua and Barbuda',  9, 0,getdate(),1 , 1268),
( 'AR', 'ARGENTINA', 'Argentina',  10, 0,getdate(),1 , 54),
( 'AM', 'ARMENIA', 'Armenia',  11, 0,getdate(),1 , 374),
( 'AW', 'ARUBA', 'Aruba',12, 0,getdate(),1 , 297),
( 'AU', 'AUSTRALIA', 'Australia', 13, 0,getdate(),1 , 61),
( 'AT', 'AUSTRIA', 'Austria',14, 0,getdate(),1 , 43),
( 'AZ', 'AZERBAIJAN', 'Azerbaijan', 15, 0,getdate(),1 , 994),
( 'BS', 'BAHAMAS', 'Bahamas',16, 0,getdate(),1 , 1242),
( 'BH', 'BAHRAIN', 'Bahrain',17, 0,getdate(),1 , 973),
( 'BD', 'BANGLADESH', 'Bangladesh',18, 0,getdate(),1 , 880),
( 'BB', 'BARBADOS', 'Barbados',19, 0,getdate(),1 , 1246),
( 'BY', 'BELARUS', 'Belarus',20, 0,getdate(),1 , 375),
( 'BE', 'BELGIUM', 'Belgium',21, 0,getdate(),1 , 32),
( 'BZ', 'BELIZE', 'Belize', 22, 0,getdate(),1 , 501),
( 'BJ', 'BENIN', 'Benin',23, 0,getdate(),1 , 229),
( 'BM', 'BERMUDA', 'Bermuda',24, 0,getdate(),1 , 1441),
( 'BT', 'BHUTAN', 'Bhutan',25, 0,getdate(),1 , 975),
( 'BO', 'BOLIVIA', 'Bolivia',26, 0,getdate(),1 , 591),
( 'BA', 'BOSNIA AND HERZEGOVINA', 'Bosnia and Herzegovina',27, 0,getdate(),1 , 387),
( 'BW', 'BOTSWANA', 'Botswana',28, 0,getdate(),1 , 267),
( 'BV', 'BOUVET ISLAND', 'Bouvet Island',29, 0,getdate(),1 , 0),
( 'BR', 'BRAZIL', 'Brazil',30, 0,getdate(),1 , 55),
( 'IO', 'BRITISH INDIAN OCEAN TERRITORY', 'British Indian Ocean Territory',31, 0,getdate(),1 , 246),
( 'BN', 'BRUNEI DARUSSALAM', 'Brunei Darussalam',32, 0,getdate(),1 , 673),
( 'BG', 'BULGARIA', 'Bulgaria',33, 0,getdate(),1 , 359),
( 'BF', 'BURKINA FASO', 'Burkina Faso',34, 0,getdate(),1 , 226),
( 'BI', 'BURUNDI', 'Burundi',35, 0,getdate(),1 , 257),
( 'KH', 'CAMBODIA', 'Cambodia',36, 0,getdate(),1 , 855),
( 'CM', 'CAMEROON', 'Cameroon',37, 0,getdate(),1 , 237),
( 'CA', 'CANADA', 'Canada',38, 0,getdate(),1 , 1),
( 'CV', 'CAPE VERDE', 'Cape Verde',39, 0,getdate(),1 , 238),
( 'KY', 'CAYMAN ISLANDS', 'Cayman Islands',40, 0,getdate(),1 , 1345),
( 'CF', 'CENTRAL AFRICAN REPUBLIC', 'Central African Republic',41, 0,getdate(),1 , 236),
( 'TD', 'CHAD', 'Chad',42, 0,getdate(),1 , 235),
( 'CL', 'CHILE', 'Chile',43, 0,getdate(),1 , 56),
( 'CN', 'CHINA', 'China',44, 0,getdate(),1 , 86),
( 'CX', 'CHRISTMAS ISLAND', 'Christmas Island',45, 0,getdate(),1 , 61),
( 'CC', 'COCOS (KEELING) ISLANDS', 'Cocos (Keeling) Islands',46, 0,getdate(),1 , 672),
( 'CO', 'COLOMBIA', 'Colombia',47, 0,getdate(),1 , 57),
( 'KM', 'COMOROS', 'Comoros',48, 0,getdate(),1 , 269),
( 'CG', 'CONGO', 'Congo',49, 0,getdate(),1 , 242),
( 'CD', 'CONGO, THE DEMOCRATIC REPUBLIC OF THE', 'Congo, the Democratic Republic of the',50, 0,getdate(),1 , 242),
( 'CK', 'COOK ISLANDS', 'Cook Islands',51, 0,getdate(),1 , 682),
( 'CR', 'COSTA RICA', 'Costa Rica',52, 0,getdate(),1 , 506),
( 'CI', 'COTE D''IVOIRE', 'Cote D''Ivoire',53, 0,getdate(),1 , 225),
( 'HR', 'CROATIA', 'Croatia',54, 0,getdate(),1 , 385),
( 'CU', 'CUBA', 'Cuba',55, 0,getdate(),1 , 53),
( 'CY', 'CYPRUS', 'Cyprus',56, 0,getdate(),1 , 357),
( 'CZ', 'CZECH REPUBLIC', 'Czech Republic',57, 0,getdate(),1 , 420),
( 'DK', 'DENMARK', 'Denmark',58, 0,getdate(),1 , 45),
( 'DJ', 'DJIBOUTI', 'Djibouti',59, 0,getdate(),1 , 253),
( 'DM', 'DOMINICA', 'Dominica',60, 0,getdate(),1 , 1767),
( 'DO', 'DOMINICAN REPUBLIC', 'Dominican Republic',61, 0,getdate(),1 ,  1809),
( 'EC', 'ECUADOR', 'Ecuador',62, 0,getdate(),1 , 593),
( 'EG', 'EGYPT', 'Egypt',63, 0,getdate(),1 ,  20),
( 'SV', 'EL SALVADOR', 'El Salvador',64, 0,getdate(),1 ,  503),
( 'GQ', 'EQUATORIAL GUINEA', 'Equatorial Guinea',65, 0,getdate(),1 ,  240),
( 'ER', 'ERITREA', 'Eritrea',66, 0,getdate(),1 , 291),
( 'EE', 'ESTONIA', 'Estonia',67, 0,getdate(),1 ,372),
( 'ET', 'ETHIOPIA', 'Ethiopia',68, 0,getdate(),1 , 251),
( 'FK', 'FALKLAND ISLANDS (MALVINAS)', 'Falkland Islands (Malvinas)',69, 0,getdate(),1 , 500),
( 'FO', 'FAROE ISLANDS', 'Faroe Islands',70, 0,getdate(),1 , 298),
( 'FJ', 'FIJI', 'Fiji',71, 0,getdate(),1 , 679),
( 'FI', 'FINLAND', 'Finland',72, 0,getdate(),1 , 358),
( 'FR', 'FRANCE', 'France',73, 0,getdate(),1 , 33),
( 'GF', 'FRENCH GUIANA', 'French Guiana',74, 0,getdate(),1 , 594),
( 'PF', 'FRENCH POLYNESIA', 'French Polynesia',75, 0,getdate(),1 , 689),
( 'TF', 'FRENCH SOUTHERN TERRITORIES', 'French Southern Territories',76, 0,getdate(),1 , 0),
( 'GA', 'GABON', 'Gabon',77, 0,getdate(),1 , 241),
( 'GM', 'GAMBIA', 'Gambia',78, 0,getdate(),1 ,220),
( 'GE', 'GEORGIA', 'Georgia',79, 0,getdate(),1 , 995),
( 'DE', 'GERMANY', 'Germany',80, 0,getdate(),1 , 49),
( 'GH', 'GHANA', 'Ghana',81, 0,getdate(),1 , 233),
( 'GI', 'GIBRALTAR', 'Gibraltar',82, 0,getdate(),1 , 350),
( 'GR', 'GREECE', 'Greece',83, 0,getdate(),1 , 30),
( 'GL', 'GREENLAND', 'Greenland',84, 0,getdate(),1 , 299),
( 'GD', 'GRENADA', 'Grenada',85, 0,getdate(),1 , 1473),
( 'GP', 'GUADELOUPE', 'Guadeloupe',86, 0,getdate(),1 , 590),
( 'GU', 'GUAM', 'Guam',87, 0,getdate(),1 , 1671),
( 'GT', 'GUATEMALA', 'Guatemala',88, 0,getdate(),1 , 502),
( 'GN', 'GUINEA', 'Guinea',89, 0,getdate(),1 , 224),
( 'GW', 'GUINEA-BISSAU', 'Guinea-Bissau',90, 0,getdate(),1 , 245),
( 'GY', 'GUYANA', 'Guyana',91, 0,getdate(),1 ,592),
( 'HT', 'HAITI', 'Haiti',92, 0,getdate(),1 , 509),
( 'HM', 'HEARD ISLAND AND MCDONALD ISLANDS', 'Heard Island and Mcdonald Islands',93, 0,getdate(),1 , 0),
( 'VA', 'HOLY SEE (VATICAN CITY STATE)', 'Holy See (Vatican City State)',94, 0,getdate(),1 , 39),
( 'HN', 'HONDURAS', 'Honduras',95, 0,getdate(),1 , 504),
( 'HK', 'HONG KONG', 'Hong Kong',96, 0,getdate(),1 , 852),
( 'HU', 'HUNGARY', 'Hungary',97, 0,getdate(),1 ,36),
( 'IS', 'ICELAND', 'Iceland',98, 0,getdate(),1 , 354),
( 'IN', 'INDIA', 'India',99, 0,getdate(),1 , 91),
( 'ID', 'INDONESIA', 'Indonesia',100, 0,getdate(),1 , 62),
( 'IR', 'IRAN, ISLAMIC REPUBLIC OF', 'Iran, Islamic Republic of',101, 0,getdate(),1 , 98),
( 'IQ', 'IRAQ', 'Iraq',102, 0,getdate(),1 ,964),
( 'IE', 'IRELAND', 'Ireland',103, 0,getdate(),1 , 353),
( 'IL', 'ISRAEL', 'Israel',104, 0,getdate(),1 , 972),
( 'IT', 'ITALY', 'Italy',105, 0,getdate(),1 , 39),
( 'JM', 'JAMAICA', 'Jamaica',106, 0,getdate(),1 , 1876),
( 'JP', 'JAPAN', 'Japan',107, 0,getdate(),1 , 81),
( 'JO', 'JORDAN', 'Jordan',108, 0,getdate(),1 , 962),
( 'KZ', 'KAZAKHSTAN', 'Kazakhstan',109, 0,getdate(),1 ,7),
( 'KE', 'KENYA', 'Kenya',110, 0,getdate(),1 , 254),
( 'KI', 'KIRIBATI', 'Kiribati',111, 0,getdate(),1 , 686),
( 'KP', 'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF', 'Korea, Democratic People''s Republic of',112, 0,getdate(),1 , 850),
( 'KR', 'KOREA, REPUBLIC OF', 'Korea, Republic of',113, 0,getdate(),1 , 82),
( 'KW', 'KUWAIT', 'Kuwait',114, 0,getdate(),1 , 965),
( 'KG', 'KYRGYZSTAN', 'Kyrgyzstan',115, 0,getdate(),1 , 996),
( 'LA', 'LAO PEOPLE''S DEMOCRATIC REPUBLIC', 'Lao People''s Democratic Republic',116, 0,getdate(),1 , 856),
( 'LV', 'LATVIA', 'Latvia',117, 0,getdate(),1 ,371),
( 'LB', 'LEBANON', 'Lebanon',118, 0,getdate(),1 , 961),
( 'LS', 'LESOTHO', 'Lesotho',119, 0,getdate(),1 , 266),
( 'LR', 'LIBERIA', 'Liberia',120, 0,getdate(),1 ,231),
( 'LY', 'LIBYAN ARAB JAMAHIRIYA', 'Libyan Arab Jamahiriya',121, 0,getdate(),1 , 218),
( 'LI', 'LIECHTENSTEIN', 'Liechtenstein',122, 0,getdate(),1 , 423),
( 'LT', 'LITHUANIA', 'Lithuania',123, 0,getdate(),1 , 370),
( 'LU', 'LUXEMBOURG', 'Luxembourg',124, 0,getdate(),1 , 352),
( 'MO', 'MACAO', 'Macao',125, 0,getdate(),1 , 853),
( 'MK', 'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF', 'Macedonia, the Former Yugoslav Republic of',126, 0,getdate(),1 , 389),
( 'MG', 'MADAGASCAR', 'Madagascar',127, 0,getdate(),1 , 261),
( 'MW', 'MALAWI', 'Malawi',128, 0,getdate(),1 , 265),
( 'MY', 'MALAYSIA', 'Malaysia',129, 0,getdate(),1 , 60),
( 'MV', 'MALDIVES', 'Maldives',130, 0,getdate(),1 , 960),
( 'ML', 'MALI', 'Mali',131, 0,getdate(),1 , 223),
( 'MT', 'MALTA', 'Malta',132, 0,getdate(),1 ,356),
( 'MH', 'MARSHALL ISLANDS', 'Marshall Islands',133, 0,getdate(),1 , 692),
( 'MQ', 'MARTINIQUE', 'Martinique',134, 0,getdate(),1 , 596),
( 'MR', 'MAURITANIA', 'Mauritania',135, 0,getdate(),1 , 222),
( 'MU', 'MAURITIUS', 'Mauritius',136, 0,getdate(),1 ,230),
( 'YT', 'MAYOTTE', 'Mayotte',137, 0,getdate(),1 , 269),
( 'MX', 'MEXICO', 'Mexico',138, 0,getdate(),1 , 52),
( 'FM', 'MICRONESIA, FEDERATED STATES OF', 'Micronesia, Federated States of',139, 0,getdate(),1 , 691),
( 'MD', 'MOLDOVA, REPUBLIC OF', 'Moldova, Republic of',140, 0,getdate(),1 , 373),
( 'MC', 'MONACO', 'Monaco',141, 0,getdate(),1 ,377),
( 'MN', 'MONGOLIA', 'Mongolia',142, 0,getdate(),1 , 976),
( 'MS', 'MONTSERRAT', 'Montserrat',143, 0,getdate(),1 , 1664),
( 'MA', 'MOROCCO', 'Morocco',144, 0,getdate(),1 ,212),
( 'MZ', 'MOZAMBIQUE', 'Mozambique',145, 0,getdate(),1 , 258),
( 'MM', 'MYANMAR', 'Myanmar',146, 0,getdate(),1 , 95),
( 'NA', 'NAMIBIA', 'Namibia',147, 0,getdate(),1 , 264),
( 'NR', 'NAURU', 'Nauru',148, 0,getdate(),1 , 674),
( 'NP', 'NEPAL', 'Nepal',149, 0,getdate(),1 , 977),
( 'NL', 'NETHERLANDS', 'Netherlands',150, 0,getdate(),1 ,31),
( 'AN', 'NETHERLANDS ANTILLES', 'Netherlands Antilles',151, 0,getdate(),1 , 599),
( 'NC', 'NEW CALEDONIA', 'New Caledonia',152, 0,getdate(),1 , 687),
( 'NZ', 'NEW ZEALAND', 'New Zealand',153, 0,getdate(),1 , 64),
( 'NI', 'NICARAGUA', 'Nicaragua',154, 0,getdate(),1 , 505),
( 'NE', 'NIGER', 'Niger',155, 0,getdate(),1 , 227),
( 'NG', 'NIGERIA', 'Nigeria',156, 0,getdate(),1 , 234),
( 'NU', 'NIUE', 'Niue',157, 0,getdate(),1 , 683),
( 'NF', 'NORFOLK ISLAND', 'Norfolk Island',158, 0,getdate(),1 , 672),
( 'MP', 'NORTHERN MARIANA ISLANDS', 'Northern Mariana Islands',159, 0,getdate(),1 , 1670),
( 'NO', 'NORWAY', 'Norway',160, 0,getdate(),1 , 47),
( 'OM', 'OMAN', 'Oman',161, 0,getdate(),1 , 968),
( 'PK', 'PAKISTAN', 'Pakistan',162, 0,getdate(),1 , 92),
( 'PW', 'PALAU', 'Palau',163, 0,getdate(),1 , 680),
( 'PS', 'PALESTINIAN TERRITORY, OCCUPIED', 'Palestinian Territory, Occupied',164, 0,getdate(),1 , 970),
( 'PA', 'PANAMA', 'Panama',165, 0,getdate(),1 , 507),
( 'PG', 'PAPUA NEW GUINEA', 'Papua New Guinea',166, 0,getdate(),1 , 675),
( 'PY', 'PARAGUAY', 'Paraguay',167, 0,getdate(),1 , 595),
( 'PE', 'PERU', 'Peru',168, 0,getdate(),1 , 51),
( 'PH', 'PHILIPPINES', 'Philippines',169, 0,getdate(),1 , 63),
( 'PN', 'PITCAIRN', 'Pitcairn',170, 0,getdate(),1 , 0),
( 'PL', 'POLAND', 'Poland',171, 0,getdate(),1 , 48),
( 'PT', 'PORTUGAL', 'Portugal',172, 0,getdate(),1 , 351),
( 'PR', 'PUERTO RICO', 'Puerto Rico',173, 0,getdate(),1 , 1787),
( 'QA', 'QATAR', 'Qatar',174, 0,getdate(),1 , 974),
( 'RE', 'REUNION', 'Reunion',175, 0,getdate(),1 , 262),
( 'RO', 'ROMANIA', 'Romania',176, 0,getdate(),1 , 40),
( 'RU', 'RUSSIAN FEDERATION', 'Russian Federation',177, 0,getdate(),1 , 70),
( 'RW', 'RWANDA', 'Rwanda',178, 0,getdate(),1 , 250),
( 'SH', 'SAINT HELENA', 'Saint Helena',179, 0,getdate(),1 , 290),
( 'KN', 'SAINT KITTS AND NEVIS', 'Saint Kitts and Nevis',180, 0,getdate(),1 , 1869),
( 'LC', 'SAINT LUCIA', 'Saint Lucia',181, 0,getdate(),1 , 1758),
( 'PM', 'SAINT PIERRE AND MIQUELON', 'Saint Pierre and Miquelon',182, 0,getdate(),1 , 508),
( 'VC', 'SAINT VINCENT AND THE GRENADINES', 'Saint Vincent and the Grenadines',183, 0,getdate(),1 , 1784),
( 'WS', 'SAMOA', 'Samoa',184, 0,getdate(),1 , 684),
( 'SM', 'SAN MARINO', 'San Marino',185, 0,getdate(),1 , 378),
( 'ST', 'SAO TOME AND PRINCIPE', 'Sao Tome and Principe',186, 0,getdate(),1 , 239),
( 'SA', 'SAUDI ARABIA', 'Saudi Arabia',187, 0,getdate(),1 , 966),
( 'SN', 'SENEGAL', 'Senegal',188, 0,getdate(),1 , 221),
( 'CS', 'SERBIA AND MONTENEGRO', 'Serbia and Montenegro',189, 0,getdate(),1 , 381),
( 'SC', 'SEYCHELLES', 'Seychelles',190, 0,getdate(),1 , 248),
( 'SL', 'SIERRA LEONE', 'Sierra Leone',191, 0,getdate(),1 , 232),
( 'SG', 'SINGAPORE', 'Singapore',192, 0,getdate(),1 , 65),
( 'SK', 'SLOVAKIA', 'Slovakia',193, 0,getdate(),1 , 421),
( 'SI', 'SLOVENIA', 'Slovenia',194, 0,getdate(),1 , 386),
( 'SB', 'SOLOMON ISLANDS', 'Solomon Islands',195, 0,getdate(),1 , 677),
( 'SO', 'SOMALIA', 'Somalia',196, 0,getdate(),1 , 252),
( 'ZA', 'SOUTH AFRICA', 'South Africa',197, 0,getdate(),1 , 27),
( 'GS', 'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS', 'South Georgia and the South Sandwich Islands',198, 0,getdate(),1 , 0),
( 'ES', 'SPAIN', 'Spain',199, 0,getdate(),1 , 34),
( 'LK', 'SRI LANKA', 'Sri Lanka',200, 0,getdate(),1 , 94),
( 'SD', 'SUDAN', 'Sudan',201, 0,getdate(),1 , 249),
( 'SR', 'SURINAME', 'Suriname',202, 0,getdate(),1 , 597),
( 'SJ', 'SVALBARD AND JAN MAYEN', 'Svalbard and Jan Mayen',203, 0,getdate(),1 , 47),
( 'SZ', 'SWAZILAND', 'Swaziland',204, 0,getdate(),1 , 268),
( 'SE', 'SWEDEN', 'Sweden',205, 0,getdate(),1 , 46),
( 'CH', 'SWITZERLAND', 'Switzerland',206, 0,getdate(),1 , 41),
( 'SY', 'SYRIAN ARAB REPUBLIC', 'Syrian Arab Republic',207, 0,getdate(),1 , 963),
( 'TW', 'TAIWAN, PROVINCE OF CHINA', 'Taiwan, Province of China',208, 0,getdate(),1 , 886),
( 'TJ', 'TAJIKISTAN', 'Tajikistan',209, 0,getdate(),1 , 992),
( 'TZ', 'TANZANIA, UNITED REPUBLIC OF', 'Tanzania, United Republic of',210, 0,getdate(),1 , 255),
( 'TH', 'THAILAND', 'Thailand',211, 0,getdate(),1 , 66),
( 'TL', 'TIMOR-LESTE', 'Timor-Leste',212, 0,getdate(),1 , 670),
( 'TG', 'TOGO', 'Togo',213, 0,getdate(),1 , 228),
( 'TK', 'TOKELAU', 'Tokelau',214, 0,getdate(),1 , 690),
( 'TO', 'TONGA', 'Tonga',215, 0,getdate(),1 , 676),
( 'TT', 'TRINIDAD AND TOBAGO', 'Trinidad and Tobago',216, 0,getdate(),1 , 1868),
( 'TN', 'TUNISIA', 'Tunisia',217, 0,getdate(),1 , 216),
( 'TR', 'TURKEY', 'Turkey',218, 0,getdate(),1 , 90),
( 'TM', 'TURKMENISTAN', 'Turkmenistan',219, 0,getdate(),1 , 7370),
( 'TC', 'TURKS AND CAICOS ISLANDS', 'Turks and Caicos Islands',220, 0,getdate(),1 , 1649),
( 'TV', 'TUVALU', 'Tuvalu',221, 0,getdate(),1 , 688),
( 'UG', 'UGANDA', 'Uganda',222, 0,getdate(),1 , 256),
( 'UA', 'UKRAINE', 'Ukraine',223, 0,getdate(),1 , 380),
( 'AE', 'UNITED ARAB EMIRATES', 'United Arab Emirates',224, 0,getdate(),1 , 971),
( 'GB', 'UNITED KINGDOM', 'United Kingdom',225, 0,getdate(),1 , 44),
( 'US', 'UNITED STATES', 'United States',226, 0,getdate(),1 , 1),
( 'UM', 'UNITED STATES MINOR OUTLYING ISLANDS', 'United States Minor Outlying Islands',227, 0,getdate(),1 , 1),
( 'UY', 'URUGUAY', 'Uruguay',228, 0,getdate(),1 , 598),
( 'UZ', 'UZBEKISTAN', 'Uzbekistan',229, 0,getdate(),1 , 998),
( 'VU', 'VANUATU', 'Vanuatu',230, 0,getdate(),1 , 678),
( 'VE', 'VENEZUELA', 'Venezuela',231, 0,getdate(),1 ,58),
( 'VN', 'VIET NAM', 'Viet Nam',232, 0,getdate(),1 , 84),
( 'VG', 'VIRGIN ISLANDS, BRITISH', 'Virgin Islands, British',233, 0,getdate(),1 , 1284),
( 'VI', 'VIRGIN ISLANDS, U.S.', 'Virgin Islands, U.s.',234, 0,getdate(),1 , 1340),
( 'WF', 'WALLIS AND FUTUNA', 'Wallis and Futuna',235, 0,getdate(),1 , 681),
( 'EH', 'WESTERN SAHARA', 'Western Sahara',236, 0,getdate(),1 , 212),
( 'YE', 'YEMEN', 'Yemen',237, 0,getdate(),1 , 967),
( 'ZM', 'ZAMBIA', 'Zambia',238, 0,getdate(),1 , 260),
( 'ZW', 'ZIMBABWE', 'Zimbabwe',239, 0,getdate(),1 , 263);


update info.country set ListOrder=ListOrder+1 where Isremoved=0

update info.country set ListOrder=1 where Name like 'UNITED STATES'

BEGIN TRY
BEGIN TRANSACTION
if EXISTS (SELECT * FROM info.country where Isremoved=0 )
BEGIN
----------------------------------------------------------------------------------------------------------------------
--apply +1 for incorect phonecode1-person
update person set phone1code = '+1' where isnull(phone1,'') <> '' and phone1code Not in
(select countrycode from  info.country where isnull(countrycode,'') <> '' AND Isremoved=0)

--apply + for corect phonecode1-person
update person set phone1code = CONCAT('+', phone1code) where isnull(phone1,'') <> '' and phone1code in
(select countrycode from  info.country where isnull(countrycode,'') <> '' AND Isremoved=0)
----------------------------------------------------------------------------------------------------------------------
--apply + for corect phonecode2-person
update person set phone2code = CONCAT('+', phone2code) where isnull(phone2,'') <> '' and phone2code in
(select countrycode from  info.country where isnull(countrycode,'') <> '' AND Isremoved=0)

--apply +1 for incorect phonecode2-person
update person set phone2code = '+1' where isnull(phone2,'') <> '' and  phone2code not in
(select countrycode from  info.country where isnull(countrycode,'') <> '' AND Isremoved=0)

-----------------------------------------------------------------------------------------------------------------------
--apply + for corect phonecode-personSupport
update personSupport set phonecode = CONCAT('+', phonecode) where isnull(phone,'') <> '' and phonecode in
(select countrycode from  info.country where isnull(countrycode,'') <> '' AND Isremoved=0)

--apply +1 for incorect phonecode-personSupport
update personSupport set PhoneCode = '+1' where isnull(phone,'') <> '' and phonecode NOt In 
(select countrycode from  info.country where isnull(countrycode,'') <> '' AND Isremoved=0)
----------------------------------------------------------------------------------------------------------------------

update info.Country set CountryCode = CONCAT('+',CountryCode) where ISnull(CountryCode,'')<>'' and IsRemoved = 0

----------------------------------------------------------------------------------------------------------------------
END
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210303074001_CountryCodePopulation', N'3.1.4');
END;

GO

