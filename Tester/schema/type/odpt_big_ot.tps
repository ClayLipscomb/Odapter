CREATE OR REPLACE TYPE ODPT.odpt_big_ot IS OBJECT (
	id										NUMBER,
	attr_number_id						NUMBER,  
	attr_integer						INTEGER,
	attr_int 							INT,
	attr_smallint			         SMALLINT,
	attr_number							NUMBER,
	--attr_numeric						NUMERIC,					-- postpone due to complexity
	--attr_decimal						DECIMAL,					-- postpone due to complexity
   attr_float						   FLOAT,              
	attr_double_precision			DOUBLE PRECISION,     
	attr_binary_float			      BINARY_FLOAT,
	attr_binary_double				BINARY_DOUBLE,
	
	attr_varchar						VARCHAR(1),
	attr_varchar_max					VARCHAR(4000),
	attr_varchar2						VARCHAR2(1),
	attr_varchar2_max					VARCHAR2(4000),
	attr_nvarchar2						NVARCHAR2(1),
	attr_nvarchar2_max				NVARCHAR2(2000),
	attr_char							CHAR(1),
	attr_char_max						CHAR(2000),
	attr_nchar							NCHAR(1),
	attr_nchar_max						NCHAR(1000),

	--attr_binary_integer				BINARY_INTEGER, -- not SQL type
	--attr_pls_integer					PLS_INTEGER,    -- not SQL type
	--attr_string							STRING(1),		 -- not SQL type
	--attr_boolean							BOOLEAN,			 -- not SQL type
	--attr_rowid							ROWID,          -- not SQL type
	--attr_urowid							UROWID,         -- not SQL type
	--attr_long								LONG,           -- does not compile, deprecated
	--attr_long_raw						LONG RAW,		 -- does not compile, deprecated
	
	attr_date							DATE,
	attr_timestamp						TIMESTAMP,
	attr_timestamp_w_l_time_zone  TIMESTAMP WITH LOCAL TIME ZONE,
	attr_timestamp_w_time_zone		TIMESTAMP WITH TIME ZONE,
	
	--attr_raw									RAW(1),			
	--attr_bfile								BFILE,
	--attr_blob							BLOB,
	--attr_clob							CLOB,
	--attr_nclob						NCLOB,
	--attr_xmltype						XMLTYPE,
	attr_last							INTEGER  
)
/