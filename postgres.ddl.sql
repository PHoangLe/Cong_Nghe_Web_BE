-- Data Definition Language

CREATE DATABASE menu_minder_db

CREATE TABLE permission(
	permission_id SERIAL,
	permission_name VARCHAR(100) NOT NULL,
	PRIMARY KEY (permission_id)
)

-- create enum role
CREATE TYPE tp_role AS ENUM ('STAFF', 'ADMIN');

-- create uuid-ossp module
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE account(
	account_id UUID DEFAULT uuid_generate_v4(),
	email TEXT NOT NULL UNIQUE,
	password TEXT NOT NULL,
	name VARCHAR(20) NOT NULL,
	date_of_birth DATE,	
	is_block BOOLEAN DEFAULT false,
	role tp_role NOT NULL DEFAULT 'STAFF',
	gender BOOLEAN,
	phone_number VARCHAR(20),
	avatar TEXT DEFAULT 'https://res.cloudinary.com/dwskvqnkc/image/upload/v1698909675/menu_minder_store/img_default_elml1l.jpg',
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (account_id)
)


CREATE TABLE category(
	category_id SERIAL,
	category_name VARCHAR(40) NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (category_id)
)



CREATE TABLE permit(
	permission_id INTEGER NOT NULL,
	account_id UUID NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (permission_id, account_id),
	CONSTRAINT fk_permit_account FOREIGN KEY(account_id) REFERENCES account(account_id) ON DELETE CASCADE,
	CONSTRAINT fk_permit_permission FOREIGN KEY(permission_id) REFERENCES permission(permission_id) ON DELETE CASCADE
)


CREATE TABLE RESERVATION(
	reservation_id SERIAL,
	created_by	UUID NOT NULL,
	customer_name VARCHAR(40) NOT NULL,
	reservation_time TIMESTAMP NOT NULL,
	customer_phone VARCHAR(12) NOT NULL,
	number_of_customer INTEGER NOT NULL DEFAULT 1,
	note TEXT,
	status VARCHAR(10) NOT NULL DEFAULT 'PENDING',
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (reservation_id),
	CONSTRAINT fk_reservation_account FOREIGN KEY(created_by) REFERENCES account(account_id) ON DELETE CASCADE
)



CREATE TABLE dining_table(
	table_id SERIAL,
	created_by UUID NOT NULL,
	status VARCHAR(10) NOT NULL DEFAULT 'AVAILABLE',
	table_number VARCHAR(10) NOT NULL UNIQUE,
	capacity INTEGER NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (table_id),
	CONSTRAINT fk_table_account FOREIGN KEY(created_by) REFERENCES account(account_id) ON DELETE CASCADE
)



CREATE TABLE SERVING (
	serving_id SERIAL,
	created_by	UUID NOT NULL,
	number_of_cutomer INTEGER NOT NULL,
	time_in TIMESTAMP NOT NULL,
	time_out TIMESTAMP NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (serving_id),
	CONSTRAINT fk_serving_account FOREIGN KEY(created_by) REFERENCES account(account_id) ON DELETE CASCADE
)



CREATE TABLE FEED_BACK (
	feed_back_id SERIAL,
	serving_id INTEGER NOT NULL,
	rating INTEGER NOT NULL,
	message TEXT,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (feed_back_id),
	CONSTRAINT fk_feedBack_serving FOREIGN KEY(serving_id) REFERENCES serving(serving_id) ON DELETE CASCADE
)



CREATE TABLE BILL (
	serving_id INTEGER NOT NULL,
	created_by UUID NOT NULL,
	total_price INTEGER NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (serving_id, created_by),
	CONSTRAINT fk_bill_serving FOREIGN KEY(serving_id) REFERENCES serving(serving_id) ON DELETE CASCADE,
	CONSTRAINT fk_bill_account FOREIGN KEY(created_by) REFERENCES account(account_id) ON DELETE CASCADE	
)



CREATE TABLE table_used(
	table_id INTEGER NOT NULL,
	serving_id INTEGER NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (table_id, serving_id),
	CONSTRAINT fk_tableUsed_table FOREIGN KEY(table_id) REFERENCES dining_table(table_id) ON DELETE CASCADE,	
	CONSTRAINT fk_tableUsed_serving FOREIGN KEY(serving_id) REFERENCES serving(serving_id) ON DELETE CASCADE	
)



CREATE TABLE FOOD (
	food_id SERIAL,
	category_id INTEGER NOT NULL,
	name VARCHAR(50) NOT NULL UNIQUE,
	price INTEGER NOT NULL,
	recipe TEXT,
	image TEXT,
	status VARCHAR(10) NOT NULL DEFAULT 'PENDING',
	PRIMARY KEY (food_id),	
	CONSTRAINT fk_food_category FOREIGN KEY(category_id) REFERENCES category(category_id) ON DELETE CASCADE	
)



CREATE TABLE food_order (
	food_order_id SERIAL,
	food_id INTEGER NOT NULL,
	serving_id INTEGER NOT NULL,
	quantity INTEGER NOT NULL,
	status VARCHAR(10) NOT NULL DEFAULT 'PENDING',
	note TEXT,
	price INTEGER NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (food_order_id),
	CONSTRAINT fk_foodOrder_food FOREIGN KEY(food_id) REFERENCES food(food_id) ON DELETE CASCADE,
	CONSTRAINT fk_foodOrder_serving FOREIGN KEY(serving_id) REFERENCES serving(serving_id) ON DELETE CASCADE
)


