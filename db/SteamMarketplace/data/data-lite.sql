--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4
-- Dumped by pg_dump version 14.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: Applications; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Applications" ("Id", "SteamId", "Name") VALUES ('09cd7e47-1d04-4445-b756-020ce5fd19f4', 730, NULL);
INSERT INTO public."Applications" ("Id", "SteamId", "Name") VALUES ('cd02f8a5-a1f9-4aca-8d6e-0f9ce6502837', 730, NULL);
INSERT INTO public."Applications" ("Id", "SteamId", "Name") VALUES ('d0a16e19-79d7-461c-a7ef-bbcac29dc51f', 730, NULL);


--
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('21e8cc7e-8df5-4113-b9f9-20498b651581', 'Игрок', 'Игрок', 'ea2feba0-77b3-4ef6-b49c-b1b1c02e36a1');
INSERT INTO public."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('b867520a-92db-4658-be39-84da53a601c0', 'Администратор', 'АДМИНИСТРАТОР', '873f0e02-1fc7-4682-9cbd-439041b118d3');


--
-- Data for Name: Currencies; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('0c6d22bc-cee2-47f8-8d29-444fd726e3e4', 'CAD', 'ca-CA');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('2b1ba08d-97ea-427d-b356-d3ad65e09905', 'UAH', 'uk-UA');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('314a66a0-ef8d-4529-8dd7-04342fe0c7cf', 'UZS', 'uz-UZ');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('3b157395-d7d3-46e1-bfc9-365a9a44d153', 'HKD', 'hk-HK');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('3bc13b81-2ac1-4149-9ee0-769d1f420bf8', 'SEK', 'se-SE');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('3de91537-d302-4dd7-8803-b2bf6c973d26', 'USD', 'us-US');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('3f54754b-e73c-48d7-a746-abff0e31d5eb', 'DKK', 'dk-DK');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('4f4ced7b-0623-4bf3-8fa2-4297f9779024', 'BYN', 'by-BY');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('68ae2ebb-b92b-46b7-9032-3cb80a22842c', 'BGN', 'bg-BG');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('6c75d227-bd1f-42c5-9fdc-17def5240e4a', 'BRL', 'br-BR');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('6e0379a4-ea9c-423d-9590-0bdcc0bac7dd', 'MDL', 'md-MD');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('6e1c5e6e-925f-421b-8c3f-8f4551e1ad35', 'SGD', 'sg-SG');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('6f1063d5-a35d-41e7-af1b-95e1db2fdca1', 'CHF', 'ch-CH');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('71df1ba6-67a8-4e9e-9947-9136aa3f1079', 'INR', 'in-IN');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('72616e1e-e63f-4262-863c-72140c5ef912', 'GBP', 'en-GB');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('7e6fdb58-2b26-4cf8-b89f-fdc3972ce9dc', 'KRW', 'kr-KR');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('84376203-fb2f-4871-b2fc-c462faf6cc78', 'TJS', 'tj-TJ');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('85e94c52-6b83-4ed0-8ae9-3975daa38af0', 'CZK', 'cz-CZ');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('8c46157c-adae-46e1-afac-b65ff275f60d', 'ZAR', 'za-ZA');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('8efca627-8746-49ce-9689-f0c195661ccd', 'TMT', 'tm-TM');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('a58d5766-4fbd-4f59-9dea-2d1baceda710', 'EUR', 'eu-EU');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('ac20be74-4c1a-4eaf-9196-df50879b7a44', 'NOK', 'no-NO');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('b4e37cf0-9bf7-471e-a68f-e6f0e4c4c98f', 'RON', 'ro-RO');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('b8b74ee6-d9a5-4dde-b7b6-21e4a04a6f7d', 'KZT', 'kk-KZ');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('c2d2eeab-7ab9-4d56-969a-192d9ae9538c', 'JPY', 'jp-JP');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('c76d4f7e-4f1d-47d8-a339-89f0ac7e7096', 'AMD', 'am-AM');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('cf7b0c49-42a1-483d-97f8-b88711f8546c', 'RUB', 'ru-RU');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('d64b4b93-77dd-4b39-a82f-9c012fd61924', 'PLN', 'pl-PL');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('d8b6a3e8-69e1-4905-aea1-808e4b25dd29', 'KGS', 'kg-KG');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('d8bdac45-92c3-4183-85ee-90b335d9f500', 'AZN', 'az-AZ');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('e1db9424-cf1d-442b-a236-46f461bace48', 'TRY', 'tr-TR');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('e272ff65-2a3f-448d-b0a2-553b22bc8ff1', 'HUF', 'hu-HU');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('eccbb5a2-0fe6-4c9f-930a-313ee8f2d2bb', 'AUD', 'au-AU');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('f340e01a-72e6-40c5-94bc-b7d407f54bd0', 'CNY', 'zh-CN');
INSERT INTO public."Currencies" ("Id", "Literal", "CultureInfoName") VALUES ('fecbdeae-ead2-42e2-b19e-5ad2599fa6f4', 'XDR', '???');


--
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."AspNetUsers" ("Id", "CurrencyId", "WalletBalance", "RegisteredAt", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('21f7b496-c675-4e8a-a34c-fc5ec0762fdb', 'cf7b0c49-42a1-483d-97f8-b88711f8546c', 1000000.000000, '2022-04-30 19:00:00', 'Admin', 'ADMIN', 'andrey.levchenko.2001@gmail.com', 'ANDREY.LEVCHENKO.2001@GMAIL.COM', true, 'AQAAAAEAACcQAAAAENwBXvESXldNIMVqEkBji9Y4ISnVLVSpoPxGV/OwgyE7slo3gPYDxls0FnU3xWvHPg==', '', 'ae7a2137-2348-4981-9b0f-0ff9adee5153', NULL, false, false, NULL, false, 0);


--
-- Data for Name: ExchangeRates; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('9be0870c-f0fd-49d1-911b-6b125a7e181e', 'eccbb5a2-0fe6-4c9f-930a-313ee8f2d2bb', 1.479947, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('baf13dce-0011-4d69-9db8-d2bc6ebb6c07', 'd8bdac45-92c3-4183-85ee-90b335d9f500', 1.700000, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a3c3f987-4386-412d-a1ca-3725c7a6db4b', '72616e1e-e63f-4262-863c-72140c5ef912', 0.836540, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a9fc191c-ba65-4a6b-add0-f67b558c2e38', 'c76d4f7e-4f1d-47d8-a339-89f0ac7e7096', 412.630326, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fbf4d0f7-8f3b-497e-9354-86633291f34c', '4f4ced7b-0623-4bf3-8fa2-4297f9779024', 2.583095, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('6b9a356e-d48e-461f-a540-a3417ec70089', '68ae2ebb-b92b-46b7-9032-3cb80a22842c', 1.942800, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('65598f19-cd51-40fa-9a72-2c9c0932bde6', '6c75d227-bd1f-42c5-9fdc-17def5240e4a', 5.398997, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1d62bd59-f586-4a27-9664-f21e795b0dba', 'e272ff65-2a3f-448d-b0a2-553b22bc8ff1', 409.000470, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c589f1b8-05d0-45fd-91d4-b023ca34de29', '3b157395-d7d3-46e1-bfc9-365a9a44d153', 7.835997, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('19b3cf26-e3ea-4184-b86a-aec254846ac9', '3f54754b-e73c-48d7-a746-abff0e31d5eb', 7.392104, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ef777966-6294-489c-b372-bef65eec33d9', '3de91537-d302-4dd7-8803-b2bf6c973d26', 1.000000, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c69e9209-32dd-41f0-8919-6dde9e4b3f84', 'a58d5766-4fbd-4f59-9dea-2d1baceda710', 0.998519, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('99cb66c9-d64e-46f7-803e-d8d83b2b1578', '71df1ba6-67a8-4e9e-9947-9136aa3f1079', 79.023452, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('50abd6de-60dc-4fbb-a0e7-128641b1f63f', 'b8b74ee6-d9a5-4dde-b7b6-21e4a04a6f7d', 475.911479, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('dc456534-d623-46ba-8e80-d6b0be1f1eaa', '0c6d22bc-cee2-47f8-8d29-444fd726e3e4', 1.297600, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('dd93a859-2084-4eba-8df1-c4801097749c', 'd8b6a3e8-69e1-4905-aea1-808e4b25dd29', 81.099991, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('9fae42fd-3740-4900-8e1f-ecb4dc38bc84', 'f340e01a-72e6-40c5-94bc-b7d407f54bd0', 6.743044, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('582da003-a9cd-4813-b3c3-92241f0cb0d1', '6e0379a4-ea9c-423d-9590-0bdcc0bac7dd', 19.306505, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('bb118d6b-ec0e-4292-bd43-ad1b843f666f', 'ac20be74-4c1a-4eaf-9196-df50879b7a44', 10.242197, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ef3bfa22-530e-4e70-aae0-365676023ba9', 'd64b4b93-77dd-4b39-a82f-9c012fd61924', 4.811709, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('16fef649-3055-4c01-9ac4-25af77459279', 'b4e37cf0-9bf7-471e-a68f-e6f0e4c4c98f', 4.925080, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('272f64fd-112f-4f5f-8ef3-1bf78228e8da', 'fecbdeae-ead2-42e2-b19e-5ad2599fa6f4', 0.762206, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('28ebc38d-fba9-46d1-a90f-74cb352af30c', '6e1c5e6e-925f-421b-8c3f-8f4551e1ad35', 1.395600, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e1d3b2ee-30d9-4124-ad4c-924cb2164160', '84376203-fb2f-4871-b2fc-c462faf6cc78', 10.488708, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e2e8aa02-81ec-412a-9234-ef84b1458dd2', 'e1db9424-cf1d-442b-a236-46f461bace48', 17.406977, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f96abe3a-cca7-4102-9816-4df4213f1723', '8efca627-8746-49ce-9689-f0c195661ccd', 3.500000, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('4bcd3b22-7d7e-44d7-bdbe-489ebf652015', '314a66a0-ef8d-4529-8dd7-04342fe0c7cf', 10935.192381, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c1babe32-e96e-4416-a776-4ba6788f4536', '2b1ba08d-97ea-427d-b356-d3ad65e09905', 29.463697, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5350be71-eec0-467c-aa7d-011eaf51074e', '85e94c52-6b83-4ed0-8ae9-3975daa38af0', 24.243968, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d33a17fc-8de0-4f84-8661-6581da8962c0', '3bc13b81-2ac1-4149-9ee0-769d1f420bf8', 10.590393, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('11166fc7-7ffa-4c72-91b1-c0687a92a757', '6f1063d5-a35d-41e7-af1b-95e1db2fdca1', 0.984300, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1f42a228-5d94-4438-a08a-62b63d5432f6', '8c46157c-adae-46e1-afac-b65ff275f60d', 17.037390, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f1e953d5-c4db-4dd4-8b9d-bafd496a4350', '7e6fdb58-2b26-4cf8-b89f-fdc3972ce9dc', 1312.098956, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f5ebbc18-0595-41bf-8d07-0140a823181e', 'c2d2eeab-7ab9-4d56-969a-192d9ae9538c', 137.999910, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5d165c3a-bc67-4560-b743-ee0272895fd1', 'cf7b0c49-42a1-483d-97f8-b88711f8546c', 58.256800, '2022-07-15 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('4721d84f-c2e4-4e42-a6de-a0d3bb593c4e', 'eccbb5a2-0fe6-4c9f-930a-313ee8f2d2bb', 1.470156, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a57bb03f-8c31-46ab-a97f-ab4381c7232a', 'd8bdac45-92c3-4183-85ee-90b335d9f500', 1.700002, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('0d2a64c0-4858-442a-83b3-222ec893bd12', '72616e1e-e63f-4262-863c-72140c5ef912', 0.843526, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e956bd05-c6d1-429b-bbbe-a9139c7453d8', 'c76d4f7e-4f1d-47d8-a339-89f0ac7e7096', 415.048761, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('6dbf7500-94c4-4e04-a956-de3c1a665e2f', '4f4ced7b-0623-4bf3-8fa2-4297f9779024', 2.543295, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f8a86caf-0955-462f-9e11-6cf0e78334d0', '68ae2ebb-b92b-46b7-9032-3cb80a22842c', 1.944399, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a5b3d959-bbb1-44d3-ace4-c49c91bc008a', '6c75d227-bd1f-42c5-9fdc-17def5240e4a', 5.401119, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fcd57c1f-1e76-41e3-989a-adec30a0b48f', 'e272ff65-2a3f-448d-b0a2-553b22bc8ff1', 397.830842, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('275f617e-4dcf-48ea-b3d2-1e7a47d05ec5', '3b157395-d7d3-46e1-bfc9-365a9a44d153', 7.836003, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('eb743a32-77ac-4d10-ae8a-c4a9a3cd3b06', '3f54754b-e73c-48d7-a746-abff0e31d5eb', 7.399302, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1e878e9a-cce4-45f5-abfd-800889b1e852', '3de91537-d302-4dd7-8803-b2bf6c973d26', 1.000000, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('572365ae-d52c-4973-a6ee-2dc7739b2be8', 'a58d5766-4fbd-4f59-9dea-2d1baceda710', 0.989659, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('2de696fe-6e53-4451-bb57-e2d6a1d35bf9', '71df1ba6-67a8-4e9e-9947-9136aa3f1079', 79.409779, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e8e0bc23-e7a8-4a9c-9d61-57212d451c0f', 'b8b74ee6-d9a5-4dde-b7b6-21e4a04a6f7d', 480.369609, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ba7ddbb0-1767-47ff-84f7-7070a9de50e8', '0c6d22bc-cee2-47f8-8d29-444fd726e3e4', 1.303800, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('b0137727-74ba-41d3-8408-fbe70f58cc5e', 'd8b6a3e8-69e1-4905-aea1-808e4b25dd29', 80.081098, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('0b20bb8a-0357-4d93-b3fe-1891485356bf', 'f340e01a-72e6-40c5-94bc-b7d407f54bd0', 6.712420, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('b31de9f0-8df5-4cf8-a285-782dd5fe517d', '6e0379a4-ea9c-423d-9590-0bdcc0bac7dd', 19.304169, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('3a359769-3d73-4685-8213-fa2b38becdaf', 'ac20be74-4c1a-4eaf-9196-df50879b7a44', 10.144105, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('8c103d32-782b-4104-981c-5c3279c77a0b', 'd64b4b93-77dd-4b39-a82f-9c012fd61924', 4.723504, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('30272232-69f3-4eaa-a3d7-3b4325bd536f', 'b4e37cf0-9bf7-471e-a68f-e6f0e4c4c98f', 4.865095, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fb8b737e-8b18-4e75-afb1-91730be96300', 'fecbdeae-ead2-42e2-b19e-5ad2599fa6f4', 0.763761, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e0d38c8f-9865-4cdb-8643-ae503a6ab891', '6e1c5e6e-925f-421b-8c3f-8f4551e1ad35', 1.397400, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('04b49f2d-68dd-415e-92bf-0dc6364d8f09', '84376203-fb2f-4871-b2fc-c462faf6cc78', 10.406803, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('54022527-f60b-42e9-bc30-187cc13fbba5', 'e1db9424-cf1d-442b-a236-46f461bace48', 17.473895, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('64ee0b9e-9559-45a1-ae1f-66b9486bb340', '8efca627-8746-49ce-9689-f0c195661ccd', 3.499991, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('25b82394-108f-4ad4-a94c-6155ec057251', '314a66a0-ef8d-4529-8dd7-04342fe0c7cf', 10970.181965, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('7415402d-0a2c-4ddd-9d20-5de2d3bdb7dd', '2b1ba08d-97ea-427d-b356-d3ad65e09905', 29.548582, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d1e80517-3562-42a8-8bf5-76d5a04b2b6d', '85e94c52-6b83-4ed0-8ae9-3975daa38af0', 24.413990, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('702e82e2-0727-40a0-8e08-6cf16e102662', '3bc13b81-2ac1-4149-9ee0-769d1f420bf8', 10.406593, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c0065bc0-ad45-4c0f-92e6-a2b0c8dbe0c4', '6f1063d5-a35d-41e7-af1b-95e1db2fdca1', 0.974300, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ef08e40f-89b7-4413-87c0-904ec9a8b6f3', '8c46157c-adae-46e1-afac-b65ff275f60d', 16.980111, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('3442b47d-95ea-42fb-9f01-82cea2e1f48c', '7e6fdb58-2b26-4cf8-b89f-fdc3972ce9dc', 1317.398909, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('441b8631-59e5-46bc-b8cc-30becc8abea0', 'c2d2eeab-7ab9-4d56-969a-192d9ae9538c', 138.899981, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('0307f4aa-b99f-48cb-9211-1a7d1f4e3e69', 'cf7b0c49-42a1-483d-97f8-b88711f8546c', 56.561600, '2022-07-19 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5e3e4564-1e23-4c0d-b3c8-5be500691575', 'eccbb5a2-0fe6-4c9f-930a-313ee8f2d2bb', 1.459640, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fdf2baaa-6703-46fa-a2df-4775685a7788', 'd8bdac45-92c3-4183-85ee-90b335d9f500', 1.700000, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('45311c7e-e2d6-4c46-990b-8a35f58918d5', '72616e1e-e63f-4262-863c-72140c5ef912', 0.831532, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('150cf05c-394b-4b5c-adf3-ffcea80a64d9', 'c76d4f7e-4f1d-47d8-a339-89f0ac7e7096', 415.348653, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('4ecab280-eb63-4100-b6fa-812f51a97b68', '4f4ced7b-0623-4bf3-8fa2-4297f9779024', 2.522099, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f70a7601-7e72-47a7-9821-5fc3ed87d7d9', '68ae2ebb-b92b-46b7-9032-3cb80a22842c', 1.930500, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5e4899be-ec7c-45ae-8708-d0004495eadd', '6c75d227-bd1f-42c5-9fdc-17def5240e4a', 5.367226, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('87fb2257-4512-4924-b453-2a746d49821e', 'e272ff65-2a3f-448d-b0a2-553b22bc8ff1', 389.589307, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('4ef98e1b-e07f-4117-9bec-cf732a577c95', '3b157395-d7d3-46e1-bfc9-365a9a44d153', 7.835995, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('865c1e61-9865-4c90-81b0-801b0e19349d', '3f54754b-e73c-48d7-a746-abff0e31d5eb', 7.347301, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('62142b70-d405-48e8-9366-178d8a516030', '3de91537-d302-4dd7-8803-b2bf6c973d26', 1.000000, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fb160da4-e9c8-4d12-a5f7-27210e5e7201', 'a58d5766-4fbd-4f59-9dea-2d1baceda710', 0.982119, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a028b94c-15c2-4603-9f05-c6382d042620', '71df1ba6-67a8-4e9e-9947-9136aa3f1079', 79.640106, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e42b39d9-2620-40c6-8c9b-03dcb86eccbf', 'b8b74ee6-d9a5-4dde-b7b6-21e4a04a6f7d', 484.178625, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('0a9de818-a7cd-4bdf-b8c3-238ed945d989', '0c6d22bc-cee2-47f8-8d29-444fd726e3e4', 1.294800, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('90527fd7-9f47-4aeb-92c2-bd4bddfaa065', 'd8b6a3e8-69e1-4905-aea1-808e4b25dd29', 79.614372, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('853966bb-4ee9-451f-83cd-c1fca2a44140', 'f340e01a-72e6-40c5-94bc-b7d407f54bd0', 6.713232, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d0728341-3a63-4631-826e-66d044a35e45', '6e0379a4-ea9c-423d-9590-0bdcc0bac7dd', 19.313403, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('058336e2-bce5-4979-9b39-73b7dd4a4f86', 'ac20be74-4c1a-4eaf-9196-df50879b7a44', 9.942002, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('4dcdc20b-2aaf-4911-b0f9-6fdc2b600b30', 'd64b4b93-77dd-4b39-a82f-9c012fd61924', 4.650287, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('417c034f-46d2-469e-ac63-8173f8b96e21', 'b4e37cf0-9bf7-471e-a68f-e6f0e4c4c98f', 4.819519, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('2e27b345-45a9-4c37-be4f-7cc7d89c2b21', 'fecbdeae-ead2-42e2-b19e-5ad2599fa6f4', 0.760294, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('958c0a39-ea93-49e6-8c5c-bc8a064d4550', '6e1c5e6e-925f-421b-8c3f-8f4551e1ad35', 1.397098, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f4941d12-ce40-4b4a-8ad9-0c067413ee98', '84376203-fb2f-4871-b2fc-c462faf6cc78', 10.379808, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('195d6927-341b-4e7c-a604-f3daed5c3f32', 'e1db9424-cf1d-442b-a236-46f461bace48', 17.479301, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fc309f45-52b4-47d6-86ae-67e1f0604bc2', '8efca627-8746-49ce-9689-f0c195661ccd', 3.500009, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('b9eebdd5-02ee-44a2-b19b-8a894dc6b73b', '314a66a0-ef8d-4529-8dd7-04342fe0c7cf', 10949.912894, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e1ce8c7a-f367-4692-8584-37c728483e38', '2b1ba08d-97ea-427d-b356-d3ad65e09905', 29.547962, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1f03e530-c64e-466c-b3ec-13fd63ebd810', '85e94c52-6b83-4ed0-8ae9-3975daa38af0', 24.192978, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('9aaea3ba-5cf2-407f-9b50-014b9217f5d3', '3bc13b81-2ac1-4149-9ee0-769d1f420bf8', 10.308605, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('0c2af607-68be-49de-914a-ba7f32c1cba5', '6f1063d5-a35d-41e7-af1b-95e1db2fdca1', 0.969901, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('7e9c5301-f381-4d82-a4ea-6ccd70cc6e57', '8c46157c-adae-46e1-afac-b65ff275f60d', 17.026681, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('9be3759d-cb27-409f-991c-d33cdcd3b411', '7e6fdb58-2b26-4cf8-b89f-fdc3972ce9dc', 1313.399102, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('8e512cfb-386e-4f43-9ee0-65cda575e6bb', 'c2d2eeab-7ab9-4d56-969a-192d9ae9538c', 138.219997, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('242b82c2-b808-4c05-9626-4ecb9cdc65ab', 'cf7b0c49-42a1-483d-97f8-b88711f8546c', 55.437000, '2022-07-20 13:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('2c223cc1-f827-40da-9312-a146617777af', 'eccbb5a2-0fe6-4c9f-930a-313ee8f2d2bb', 1.446759, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('8ad295d5-56b4-41c8-9734-c0ab65bc0870', 'd8bdac45-92c3-4183-85ee-90b335d9f500', 1.699999, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('dc864f6e-3bd6-4d99-b13e-f220b32cdbe9', '72616e1e-e63f-4262-863c-72140c5ef912', 0.831463, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('21d0d977-f7d9-4253-bae9-831eaabb5a6c', 'c76d4f7e-4f1d-47d8-a339-89f0ac7e7096', 415.599048, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('bcb7c427-3935-4542-85c1-44c3c1bf347c', '4f4ced7b-0623-4bf3-8fa2-4297f9779024', 2.506402, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('49ea565e-efd4-4b06-9063-fcaeb8e251f0', '68ae2ebb-b92b-46b7-9032-3cb80a22842c', 1.909103, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a5ad853c-2ca3-4a13-a1ed-c22fcdd1ee49', '6c75d227-bd1f-42c5-9fdc-17def5240e4a', 5.390098, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('57eca399-849f-4ec6-80e4-6955a04fc663', 'e272ff65-2a3f-448d-b0a2-553b22bc8ff1', 388.270980, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('687881ae-e9b9-4a2a-a31f-d094706e1e76', '3b157395-d7d3-46e1-bfc9-365a9a44d153', 7.836000, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ce737eab-22f3-42e6-b17a-498ca90687e3', '3f54754b-e73c-48d7-a746-abff0e31d5eb', 7.266899, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('217ad036-679b-43b9-bdc1-7944bbfc6a6f', '3de91537-d302-4dd7-8803-b2bf6c973d26', 1.000000, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('7a2c4d13-8267-4ee3-baa4-ff3fa34e064b', 'a58d5766-4fbd-4f59-9dea-2d1baceda710', 0.982501, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a9e694df-4f11-4c63-abdb-7289513bc2b7', '71df1ba6-67a8-4e9e-9947-9136aa3f1079', 79.659195, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('cb6edc08-d300-477f-8da1-e3c42de1cb2a', 'b8b74ee6-d9a5-4dde-b7b6-21e4a04a6f7d', 480.361350, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5c99bca5-c60e-4c9d-a8ff-bc37b0ac753a', '0c6d22bc-cee2-47f8-8d29-444fd726e3e4', 1.290400, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('950bdd4a-5294-4fa7-b0be-50e138dde1e3', 'd8b6a3e8-69e1-4905-aea1-808e4b25dd29', 79.528563, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e73eb1ed-a51f-4705-a3d4-6fe89fb28cf7', 'f340e01a-72e6-40c5-94bc-b7d407f54bd0', 6.721613, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('b6bf390a-4659-4113-a28b-4874fb3c14ba', '6e0379a4-ea9c-423d-9590-0bdcc0bac7dd', 19.313403, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c8c93826-cce1-4d90-a23d-b165902306ce', 'ac20be74-4c1a-4eaf-9196-df50879b7a44', 9.956995, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('7db32dce-9a13-42a3-bbd7-c70110306e36', 'd64b4b93-77dd-4b39-a82f-9c012fd61924', 4.686796, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ff2824c9-9a2d-42cd-abd4-2c39c6e9f100', 'b4e37cf0-9bf7-471e-a68f-e6f0e4c4c98f', 4.822196, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('26605cc4-f320-43d4-99ac-5c784174ff45', 'fecbdeae-ead2-42e2-b19e-5ad2599fa6f4', 0.757535, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('356d4083-0c4e-41e4-b6df-64ae9178aed0', '6e1c5e6e-925f-421b-8c3f-8f4551e1ad35', 1.391300, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1ed605cf-3f2e-43a9-a3c0-8835eed894dc', '84376203-fb2f-4871-b2fc-c462faf6cc78', 10.349004, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('dcf8d8c0-28e6-42b7-bbac-22125fbad1f0', 'e1db9424-cf1d-442b-a236-46f461bace48', 17.558286, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('73391f94-72bd-4bd3-88a1-8d1313df4362', '8efca627-8746-49ce-9689-f0c195661ccd', 3.499994, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a278039b-db02-44d4-a9a1-38dd260e3325', '314a66a0-ef8d-4529-8dd7-04342fe0c7cf', 10925.549673, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('08c53984-d9f9-46f5-aae7-7e2f01b913db', '2b1ba08d-97ea-427d-b356-d3ad65e09905', 29.548018, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d2fe915e-03ce-4d80-962b-dba6dd0cf2b7', '85e94c52-6b83-4ed0-8ae9-3975daa38af0', 23.969994, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e9952cab-b6b4-4f41-b26c-be2126b76045', '3bc13b81-2ac1-4149-9ee0-769d1f420bf8', 10.200802, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c3615b0f-13fc-4447-b379-46c7bd69d568', '6f1063d5-a35d-41e7-af1b-95e1db2fdca1', 0.969600, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('52b42abe-77b8-496a-b230-8d0fe6a06685', '8c46157c-adae-46e1-afac-b65ff275f60d', 17.138416, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('455491ae-78a2-427c-a898-3be31ff01632', '7e6fdb58-2b26-4cf8-b89f-fdc3972ce9dc', 1312.898693, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('096fcd88-64af-4088-aed6-355453580066', 'c2d2eeab-7ab9-4d56-969a-192d9ae9538c', 138.149894, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c5b48d05-709f-4b23-aedf-185f710893a4', 'cf7b0c49-42a1-483d-97f8-b88711f8546c', 54.849100, '2022-07-20 17:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('6f173bff-a6fb-4247-a323-8fcec396767c', 'eccbb5a2-0fe6-4c9f-930a-313ee8f2d2bb', 1.447178, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c54e96bd-1711-440e-8e2d-a7b433cae3c7', 'd8bdac45-92c3-4183-85ee-90b335d9f500', 1.700002, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('65d124ec-9575-4d6d-9540-b7e0a84ac55e', '72616e1e-e63f-4262-863c-72140c5ef912', 0.833472, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('f3b0bf09-6ff8-4cdf-9786-3c8321920ee2', 'c76d4f7e-4f1d-47d8-a339-89f0ac7e7096', 414.839326, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('da63110e-4057-413d-8d43-e4a2920e69bd', '4f4ced7b-0623-4bf3-8fa2-4297f9779024', 2.520205, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5d4adae3-4438-4b4a-ab93-44beb37c24a5', '68ae2ebb-b92b-46b7-9032-3cb80a22842c', 1.917697, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('10ff51b2-e6d8-40dd-9a05-66d8cfd37852', '6c75d227-bd1f-42c5-9fdc-17def5240e4a', 5.428518, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('6a209629-ba38-4c26-ac6f-9665a60f6871', 'e272ff65-2a3f-448d-b0a2-553b22bc8ff1', 394.060311, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('705e93ab-9263-4c90-8068-33df9b383266', '3b157395-d7d3-46e1-bfc9-365a9a44d153', 7.836002, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('654a5227-6f75-47af-82ab-b9e6eac2ca5c', '3f54754b-e73c-48d7-a746-abff0e31d5eb', 7.299899, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('fd9045db-d617-40da-b2db-2c3762cf82c1', '3de91537-d302-4dd7-8803-b2bf6c973d26', 1.000000, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d7164db9-5a71-4a5c-b808-ab490d47d8fc', 'a58d5766-4fbd-4f59-9dea-2d1baceda710', 0.988882, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('6aea6937-4e79-4458-8bc3-762796ce211e', '71df1ba6-67a8-4e9e-9947-9136aa3f1079', 79.870998, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ecdb9398-2615-413d-960b-d1d75da7dcb9', 'b8b74ee6-d9a5-4dde-b7b6-21e4a04a6f7d', 481.198773, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1c8b9c6c-f084-4bc7-b046-6c05046a2094', '0c6d22bc-cee2-47f8-8d29-444fd726e3e4', 1.288400, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('0fc3c0c1-b233-4c55-9778-1a754dab534a', 'd8b6a3e8-69e1-4905-aea1-808e4b25dd29', 79.679663, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('a2b1dc1f-3647-40f0-882a-91084103432c', 'f340e01a-72e6-40c5-94bc-b7d407f54bd0', 6.724776, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('e95b8a2f-804f-47f3-a653-9d71d513e61a', '6e0379a4-ea9c-423d-9590-0bdcc0bac7dd', 19.312914, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('3d1338d9-027a-45dc-a499-317f2c741ed4', 'ac20be74-4c1a-4eaf-9196-df50879b7a44', 9.940195, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5ce9d10f-13c1-4726-be4d-45f02bbe3044', 'd64b4b93-77dd-4b39-a82f-9c012fd61924', 4.641009, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('cfbfe63b-c513-4b8a-9677-aea4e318b7dd', 'b4e37cf0-9bf7-471e-a68f-e6f0e4c4c98f', 4.851296, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('59d0cf2e-9ecf-4607-be68-6e5a8405448a', 'fecbdeae-ead2-42e2-b19e-5ad2599fa6f4', 0.759631, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('b22a6df8-1ee3-447e-a5b2-fefa5b5785d5', '6e1c5e6e-925f-421b-8c3f-8f4551e1ad35', 1.392401, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('8667e7f3-d55e-47b8-83b8-59b0b326deba', '84376203-fb2f-4871-b2fc-c462faf6cc78', 10.272293, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('ce59b3cc-4b8b-438d-b7b7-41defdb1f0d5', 'e1db9424-cf1d-442b-a236-46f461bace48', 17.576417, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('786d54fc-396b-40b4-b443-f06c3b12023a', '8efca627-8746-49ce-9689-f0c195661ccd', 3.499991, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c04fc956-5331-4b27-8ebf-f5203d93d199', '314a66a0-ef8d-4529-8dd7-04342fe0c7cf', 10907.990034, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('7b677ffb-7df0-4d3f-8cb6-d29dee4585cc', '2b1ba08d-97ea-427d-b356-d3ad65e09905', 36.786252, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d8d30bb6-579c-4fac-a680-469d28d33d17', '85e94c52-6b83-4ed0-8ae9-3975daa38af0', 24.011964, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('1a4e8a8a-3498-4d21-aa7c-455f7bf13a67', '3bc13b81-2ac1-4149-9ee0-769d1f420bf8', 10.219709, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('774e51d2-7029-461b-a0cd-5317ce606e89', '6f1063d5-a35d-41e7-af1b-95e1db2fdca1', 0.972500, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('5fd4aafa-e8c2-4208-b3b6-15962d8962cf', '8c46157c-adae-46e1-afac-b65ff275f60d', 17.231813, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('66fc73c6-691e-49fb-9481-09a41891ccbf', '7e6fdb58-2b26-4cf8-b89f-fdc3972ce9dc', 1307.701035, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('d72846ac-f7f8-4bc9-8b0a-19e38400303e', 'c2d2eeab-7ab9-4d56-969a-192d9ae9538c', 138.449942, '2022-07-22 12:00:00');
INSERT INTO public."ExchangeRates" ("Id", "CurrencyId", "Rate", "DateTime") VALUES ('c78850cd-aa96-4851-ba9d-b38ffffd41fd', 'cf7b0c49-42a1-483d-97f8-b88711f8546c', 56.478300, '2022-07-22 12:00:00');


--
-- Data for Name: TransactionTypes; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."TransactionTypes" ("Id", "Name", "RuName") VALUES ('0fdd5521-90fe-4709-9cce-d4a7d4ffb2e1', 'Adding funds to your account', 'Пополнение');
INSERT INTO public."TransactionTypes" ("Id", "Name", "RuName") VALUES ('1f70a7dc-9dee-4e44-946d-4b82783bf77b', 'Sale', 'Продажа');
INSERT INTO public."TransactionTypes" ("Id", "Name", "RuName") VALUES ('2edbc026-1aaa-4f61-9f71-e3ab94fa6252', 'Withdrawal of funds', 'Вывод средств');
INSERT INTO public."TransactionTypes" ("Id", "Name", "RuName") VALUES ('e98b7e58-8f0e-4662-b5a7-5358e8107ef8', 'Purchase', 'Покупка');


--
-- Data for Name: Transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Transactions" ("Id", "UserId", "TypeId", "PurchaseId", "Value", "HappenedAt") VALUES ('756cd385-85a6-4eb8-a2d8-e9dfddbc98ef', '21f7b496-c675-4e8a-a34c-fc5ec0762fdb', '0fdd5521-90fe-4709-9cce-d4a7d4ffb2e1', NULL, 1000000.000000, '2022-04-30 19:05:00');


--
-- PostgreSQL database dump complete
--

