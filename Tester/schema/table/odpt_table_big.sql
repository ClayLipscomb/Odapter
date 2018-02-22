--DROP TABLE ODPT.odpt_table_big CASCADE CONSTRAINTS;

CREATE TABLE ODPT.odpt_table_big (
	id									NUMBER		NOT NULL,
	col_number_id						NUMBER,  
	col_integer							INTEGER,
	col_int 							INT,
	col_smallint						SMALLINT,
	col_number							NUMBER,
	col_double_precision				DOUBLE PRECISION,
--	col_numeric							NUMERIC,		-- implementation to be determined
--	col_decimal							DECIMAL,		-- implementation to be determined
	col_float							FLOAT,		-- FLOAT(126)
	col_binary_float					BINARY_FLOAT,
	col_binary_double					BINARY_DOUBLE,
	
	col_varchar_min						VARCHAR(1),
	col_varchar_max						VARCHAR(4000),
	col_varchar2_min					VARCHAR2(1),
	col_varchar2_max					VARCHAR2(4000),
	col_nvarchar2_min					NVARCHAR2(1),
	col_nvarchar2_max					NVARCHAR2(2000),
	col_char_min						CHAR(1),
	col_char_max						CHAR(2000),
	col_nchar_min						NCHAR(1),
	col_nchar_max						NCHAR(1000),
--	col_ref								REF odpt_big_ot,
	
--	col_binary_integer				BINARY_INTEGER,		-- not a SQL type
--	col_pls_integer					PLS_INTEGER,			-- not a SQL type
-- col_string							STRING,					--	not a SQL type
-- col_boolean							BOOLEAN,					-- not a SQL type
	
-- col_rowid							ROWID,
-- col_urowid							UROWID,
	
	col_date							DATE,
	col_timestamp						TIMESTAMP,
	col_timestamp_prec0					TIMESTAMP(0),
	col_timestamp_prec9					TIMESTAMP(9),
--	col_timestamp_t_z					TIMESTAMP WITH TIME ZONE,
--	col_timestamp_t_z_prec0			TIMESTAMP(0) WITH TIME ZONE,
--	col_timestamp_t_z_prec9			TIMESTAMP(9) WITH TIME ZONE,
--	col_timestamp_l_t_z				TIMESTAMP WITH TIME ZONE,
--	col_timestamp_l_t_z_prec0		TIMESTAMP(0) WITH TIME ZONE,
--	col_timestamp_l_t_z_prec9		TIMESTAMP(9) WITH TIME ZONE,

-- col_xmltype							XMLTYPE,
--	col_mlslabel						MLSLABEL,
--	col_raw								RAW(1),
-- col_bfile							BFILE,
-- col_blob								BLOB,
-- col_clob								CLOB,
-- col_nclob							NCLOB,
-- col_long								LONG,				-- deprecated by Oracle
-- col_long_raw						LONG RAW,		-- deprecated by Oracle
	col_last							VARCHAR2(4000)
);

CREATE UNIQUE INDEX ODPT.odpt_table_big_pk ON ODPT.odpt_table_big(id);
ALTER TABLE ODPT.odpt_table_big ADD ( CONSTRAINT odpt_table_big_pk PRIMARY KEY (id) USING INDEX ODPT.odpt_table_big_pk);
