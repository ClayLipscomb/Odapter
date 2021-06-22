--DROP TABLE ODPT.odpt_table_object CASCADE CONSTRAINTS;

CREATE TABLE ODPT.odpt_table_object (
	id					NUMBER		NOT NULL,
	col_object			odpt_big_ot,
	col_ref_object		REF odpt_big_ot,
	col_last			INTEGER
);

CREATE UNIQUE INDEX ODPT.odpt_table_object_pk ON ODPT.odpt_table_object(id);
ALTER TABLE ODPT.odpt_table_object ADD ( CONSTRAINT odpt_table_object_pk PRIMARY KEY (id) USING INDEX ODPT.odpt_table_object_pk);