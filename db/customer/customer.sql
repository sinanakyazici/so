DROP TABLE IF EXISTS customer CASCADE;

CREATE TABLE "customer" (
	id uuid NOT NULL,
	identity_id varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	nationality varchar(2) NULL,
	birthdate timestamp NULL,
	phone_number varchar(50) NULL,
	-- address
	address_country varchar(50) NULL,
	address_city varchar(50) NULL,
	address_district varchar(50) NULL,
	address_text varchar(250) NULL,
	address_zip_code varchar(50) NULL,
 
	creation_time timestamp NOT NULL,
	creator_name varchar(50) NOT NULL,
	last_modification_time timestamp NULL,
	last_modifier_name varchar(50) NULL,
	valid_for timestamp NULL,
	CONSTRAINT customer_pk PRIMARY KEY (id)
);

INSERT INTO "customer" (id, identity_id, email, first_name, last_name, nationality, birthdate, phone_number, address_country, address_city, address_district, address_text, address_zip_code, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('78000985-c789-439a-9714-821f36c9c051', '99545622266', 'sinan_akyazici@hotmail.com', 'Sinan', 'AKYAZICI', 'TR', '1987-05-22 00:00:00.000000', '+909995648788', 'Türkiye', 'İstanbul', 'Beylikdüzü', 'Orman mah. Karakdeniz cad. Ata Sitesi B Blok Kat 2 Daire 9', '34524', '2023-01-15 13:02:50.477296', 'unknown', null, null, null);
