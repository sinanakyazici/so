DROP TABLE IF EXISTS "order" CASCADE;

CREATE TABLE "order" (
	id uuid NOT NULL,
	customer_id uuid NOT NULL,
	order_total_price money NOT NULL,
	notes text NULL,
	-- adress
	address_country varchar(50) NULL,
	address_city varchar(50) NULL,
	address_district varchar(50) NULL,
	address_text varchar(250) NULL,
	address_zip_code varchar(50) NULL,
	-- address
	creation_time timestamp NOT NULL,
	creator_name varchar(50) NOT NULL,
	last_modification_time timestamp NULL,
	last_modifier_name varchar(50) NULL,
	valid_for timestamp NULL,
	CONSTRAINT order_pk PRIMARY KEY (id)
);

INSERT INTO "order" (id, customer_id, order_total_price, notes, address_country, address_city, address_district, address_text, address_zip_code, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('1d27453f-ef60-488d-8c2e-73f1e9f87058', '78000985-c789-439a-9714-821f36c9c051', '$243.00', 'Bu bir test siparişidir.', 'Türkiye', 'İstanbul', 'Beylikdüzü', 'Sahil mah. Mavi Akdeniz cad. Liv Marine Sitesi B1-B Blok Kat 2 Daire 8', '34523', '2023-01-15 16:03:50.148640', 'unknown', null, null, null);
 

DROP TABLE IF EXISTS "order_item" CASCADE;

CREATE TABLE "order_item" (
	id uuid NOT NULL,
	order_id uuid NOT NULL,
	product_id uuid NOT NULL,
	product_name varchar(250) NULL,
	unit_price money NOT NULL,
	quantity int4 NOT NULL,
	creation_time timestamp NOT NULL,
	creator_name varchar(50) NOT NULL,
	last_modification_time timestamp NULL,
	last_modifier_name varchar(50) NULL,
	valid_for timestamp NULL,
	CONSTRAINT order_item_pk PRIMARY KEY (id),
	CONSTRAINT order_item_unique UNIQUE (id, product_id),
	CONSTRAINT order_item_fk FOREIGN KEY (order_id) REFERENCES "order"(id)
);

INSERT INTO order_item (id, order_id, product_id, product_name, unit_price, quantity, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('495d8558-3983-4537-a823-0ffb99671c87', '1d27453f-ef60-488d-8c2e-73f1e9f87058', 'a9c3b304-60c9-4ab2-a8d5-5b670547cc8b', 'Test Ürünü 3', '27.00', 1, '2023-01-15 16:03:50.149219', 'unknown', null, null, null);
INSERT INTO order_item (id, order_id, product_id, product_name, unit_price, quantity, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('5b468b18-0e67-42e0-badb-243e268419a0', '1d27453f-ef60-488d-8c2e-73f1e9f87058', '527a9de7-ddec-422d-a00f-52e095914ca1', 'Test Ürünü 1', '38.00', 4, '2023-01-15 16:03:50.149214', 'unknown', null, null, null);
INSERT INTO order_item (id, order_id, product_id, product_name, unit_price, quantity, creation_time, creator_name, last_modification_time, last_modifier_name, valid_for) VALUES ('9c51cd42-b98c-42bb-8069-e0bb2582c5ae', '1d27453f-ef60-488d-8c2e-73f1e9f87058', '527a9de7-ddec-422d-a00f-52e095914ca1', 'Test Ürünü 2', '12.00', 7, '2023-01-15 16:03:50.149218', 'unknown', null, null, null);
