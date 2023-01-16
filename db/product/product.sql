DROP TABLE IF EXISTS product_type CASCADE;

CREATE TABLE "product_type" (
	id uuid NOT NULL,
	name varchar(50) NOT NULL,
	description varchar(50) NULL,
 
	creation_time timestamp NOT NULL,
	creator_name varchar(50) NOT NULL,
	last_modification_time timestamp NULL,
	last_modifier_name varchar(50) NULL,
	valid_for timestamp NULL,
	CONSTRAINT product_type_pk PRIMARY KEY (id)
);

INSERT INTO public.product_type (id, name, description, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('23085c07-7c1b-49e6-a189-e3a07c1b502b', 'Kirtasiye', 'Kirtasiye Malzemeleri', '2023-01-13 23:44:22.000000', 'unknown', null, null, null);


DROP TABLE IF EXISTS product CASCADE;

CREATE TABLE "product" (
	id uuid NOT NULL,
	product_type_id uuid NOT NULL,
	name varchar(50) NOT NULL,
	product_code varchar(50) NOT NULL,
 
	creation_time timestamp NOT NULL,
	creator_name varchar(50) NOT NULL,
	last_modification_time timestamp NULL,
	last_modifier_name varchar(50) NULL,
	valid_for timestamp NULL,
	CONSTRAINT product_pk PRIMARY KEY (id)
);

INSERT INTO public.product (id, product_type_id, name, product_code, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('527a9de7-ddec-422d-a00f-52e095914ca1', '23085c07-7c1b-49e6-a189-e3a07c1b502b', 'Kitap', 'SO-0001', '2023-01-13 20:41:38.911190', 'unknown', null, null, null);
INSERT INTO public.product (id, product_type_id, name, product_code, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('a9c3b304-60c9-4ab2-a8d5-5b670547cc8b', '23085c07-7c1b-49e6-a189-e3a07c1b502b', 'Kalem', 'SO-0002', '2023-01-13 20:41:48.177918', 'unknown', null, null, null);

