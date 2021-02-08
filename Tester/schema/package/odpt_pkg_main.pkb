CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_main AS

	FUNCTION get_ref_cur_rec_fld_same_name RETURN t_ref_cur_rec_fld_same_name IS	
		l_cursor	t_ref_cur_rec_fld_same_name;
	BEGIN
		RETURN l_cursor;
	END;	

    PROCEDURE proc_underscore_suffix IS
	BEGIN
        RETURN;
	END;

    PROCEDURE proc_underscore_suffix_ IS
	BEGIN
        RETURN;
	END;

    PROCEDURE proc_raise_exception IS
    BEGIN        
        RAISE_APPLICATION_ERROR(-9999, 'Exception');
    END;

    PROCEDURE proc_nocopy_increment(p_in IN INTEGER, p_in_out_nocopy IN OUT NOCOPY INTEGER, p_out_nocopy OUT NOCOPY INTEGER) IS
    BEGIN   
		p_in_out_nocopy := p_in_out_nocopy + 1;
		p_out_nocopy := p_in + 1;
        RETURN;
    END;

	PROCEDURE proc_no_param IS
	BEGIN
		RETURN;
	END;
 
	FUNCTION func_no_param RETURN NUMBER IS
	BEGIN
		RETURN 0;
	END;

    PROCEDURE dup_signature(p_param_in1 IN INTEGER, p_param_in_out1 IN OUT INTEGER, p_param_out1 OUT INTEGER) IS
    BEGIN
  		p_param_in_out1 := p_param_in1 + 1;
      	p_param_out1 := p_param_in1 + 1;    
        RETURN;
    END;

    PROCEDURE dup_signature(p_param_in2 IN INTEGER, p_param_in_out2 IN OUT INTEGER, p_param_out2 OUT INTEGER) IS
    BEGIN
  		p_param_in_out2 := p_param_in2 + 2;
      	p_param_out2 := p_param_in2 + 2;    
        RETURN;
    END;

    PROCEDURE dup_signature(p_param_in3 IN INTEGER, p_param_in_out3 IN OUT INTEGER, p_param_out3 OUT INTEGER) IS
    BEGIN
  		p_param_in_out3 := p_param_in3 + 3;
      	p_param_out3 := p_param_in3 + 3;    
        RETURN;
    END;

    FUNCTION dup_signature(p_param_in1 IN INTEGER, p_param_in_out1 IN OUT INTEGER, p_param_out1 OUT INTEGER) RETURN INTEGER IS
    BEGIN
  		p_param_in_out1 := p_param_in1 + 1;
      	p_param_out1 := p_param_in1 + 1;    
        RETURN p_param_in1 + 1;
    END;

    FUNCTION dup_signature(p_param_in2 IN INTEGER, p_param_in_out2 IN OUT INTEGER, p_param_out2 OUT INTEGER) RETURN INTEGER IS
    BEGIN
  		p_param_in_out2 := p_param_in2 + 2;
      	p_param_out2 := p_param_in2 + 2;    
        RETURN p_param_in2 + 2;
    END;

    FUNCTION dup_signature(p_param_in3 IN INTEGER, p_param_in_out3 IN OUT INTEGER, p_param_out3 OUT INTEGER) RETURN INTEGER IS
    BEGIN
  		p_param_in_out3 := p_param_in3 + 3;
      	p_param_out3 := p_param_in3 + 3;   
        RETURN p_param_in3 + 3;    
    END;       
    
    FUNCTION dup_signature_translated_str(p_param_in IN VARCHAR2, p_param_in_out IN OUT VARCHAR2, p_param_out OUT VARCHAR2) RETURN VARCHAR2 IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;
    END; 

    FUNCTION dup_signature_translated_str(p_param_in IN NVARCHAR2, p_param_in_out IN OUT NVARCHAR2, p_param_out OUT NVARCHAR2) RETURN NVARCHAR2 IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;   
    END; 

    FUNCTION dup_signature_translated_str(p_param_in IN CHAR, p_param_in_out IN OUT CHAR, p_param_out OUT CHAR) RETURN CHAR IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;    
    END; 

    FUNCTION dup_signature_translated_str(p_param_in IN NCHAR, p_param_in_out IN OUT NCHAR, p_param_out OUT NCHAR) RETURN NCHAR IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;    
    END; 

    FUNCTION dup_signature_translated_str(p_param_in IN CLOB, p_param_in_out IN OUT CLOB, p_param_out OUT CLOB) RETURN CLOB IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;     
    END; 

    FUNCTION dup_signature_translated_str(p_param_in IN NCLOB, p_param_in_out IN OUT NCLOB, p_param_out OUT NCLOB) RETURN NCLOB IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;     
    END; 

    FUNCTION dup_signature_translated_date(p_param_in IN DATE, p_param_in_out IN OUT DATE, p_param_out OUT DATE) RETURN DATE IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;    
    END; 

    FUNCTION dup_signature_translated_date(p_param_in IN TIMESTAMP, p_param_in_out IN OUT TIMESTAMP, p_param_out OUT TIMESTAMP) RETURN TIMESTAMP IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;  
    END; 

    FUNCTION dup_sig_translated_byte_arr(p_param_in IN LONG, p_param_in_out IN OUT LONG, p_param_out OUT LONG) RETURN LONG IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;     
    END; 

    FUNCTION dup_sig_translated_byte_arr(p_param_in IN BFILE, p_param_in_out IN OUT BFILE, p_param_out OUT BFILE) RETURN BFILE IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;     
    END; 

    FUNCTION dup_sig_translated_byte_arr(p_param_in IN BLOB, p_param_in_out IN OUT BLOB, p_param_out OUT BLOB) RETURN BLOB IS
    BEGIN
  		p_param_in_out := p_param_in;
      	p_param_out := p_param_in;    
        RETURN p_param_in;  
    END; 

    FUNCTION dup_sig_translated_byte_arr(p_param_in IN RAW, p_param_in_out IN OUT RAW, p_param_out OUT RAW) RETURN RAW IS
    BEGIN
  		p_param_in_out := p_param_in; 
      	p_param_out := p_param_in;    
        RETURN p_param_in;  
    END; 

	PROCEDURE proc_optional_param(p_in_number_required IN NUMBER, p_in_out_number_required IN OUT NUMBER, p_in_number_optional IN NUMBER DEFAULT 0, p_in_varchar2_optional IN VARCHAR2 DEFAULT 'TEST') IS
	BEGIN
		p_in_out_number_required := p_in_number_required;
		RETURN;
	END;

	PROCEDURE proc_optional_param_reversed(p_in_number_optional IN NUMBER DEFAULT 0, p_in_varchar2_optional IN VARCHAR2 DEFAULT 'TEST', p_in_number_required IN NUMBER, p_in_out_number_required IN OUT NUMBER) IS
	BEGIN
		p_in_out_number_required := p_in_number_required;
		RETURN;
	END;

	FUNCTION func_optional_param(p_in_number_required IN NUMBER, p_in_out_number_required IN OUT NUMBER, p_in_number_optional IN NUMBER DEFAULT 0, p_in_varchar2_optional IN VARCHAR2 DEFAULT 'TEST') RETURN NUMBER IS
	BEGIN
		p_in_out_number_required := p_in_number_required;
		RETURN p_in_out_number_required;
	END;

	FUNCTION func_optional_param_reversed(p_in_number_optional IN NUMBER DEFAULT 0, p_in_varchar2_optional IN VARCHAR2 DEFAULT 'TEST', p_in_number_required IN NUMBER, p_in_out_number_required IN OUT NUMBER) RETURN NUMBER IS
	BEGIN
		p_in_out_number_required := p_in_number_required;
		RETURN p_in_out_number_required; 
	END;

	FUNCTION func_integer(p_in IN INTEGER, p_in_out IN OUT INTEGER, p_out OUT INTEGER) RETURN INTEGER IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_int(p_in IN INT, p_in_out IN OUT INT, p_out OUT INT) RETURN INT IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_smallint(p_in IN SMALLINT, p_in_out IN OUT SMALLINT, p_out OUT SMALLINT) RETURN SMALLINT IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_string(p_in IN STRING, p_in_out IN OUT STRING, p_out OUT STRING) RETURN STRING IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_nchar(p_in IN NCHAR, p_in_out IN OUT NCHAR, p_out OUT NCHAR) RETURN NCHAR IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_varchar(p_in IN VARCHAR, p_in_out IN OUT VARCHAR, p_out OUT VARCHAR) RETURN VARCHAR IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_varchar2(p_in IN VARCHAR2, p_in_out IN OUT VARCHAR2, p_out OUT VARCHAR2) RETURN VARCHAR2 IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_nvarchar2(p_in IN NVARCHAR2, p_in_out IN OUT NVARCHAR2, p_out OUT NVARCHAR2) RETURN NVARCHAR2 IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_char(p_in IN CHAR, p_in_out IN OUT CHAR, p_out OUT CHAR) RETURN CHAR IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_ref(p_in IN REF odpt_big_ot, p_in_out IN OUT REF odpt_big_ot, p_out OUT REF odpt_big_ot) RETURN REF odpt_big_ot IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_number(p_in IN NUMBER, p_in_out IN OUT NUMBER, p_out OUT NUMBER) RETURN NUMBER IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_numeric(p_in IN NUMERIC, p_in_out IN OUT NUMERIC, p_out OUT NUMERIC) RETURN NUMERIC IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_decimal(p_in IN DECIMAL, p_in_out IN OUT DECIMAL, p_out OUT DECIMAL) RETURN DECIMAL IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_binary_integer(p_in IN BINARY_INTEGER, p_in_out IN OUT BINARY_INTEGER, p_out OUT BINARY_INTEGER) RETURN BINARY_INTEGER IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_pls_integer(p_in IN PLS_INTEGER, p_in_out IN OUT PLS_INTEGER, p_out OUT PLS_INTEGER) RETURN PLS_INTEGER IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_natural(p_in IN NATURAL, p_in_out IN OUT NATURAL, p_out OUT NATURAL) RETURN NATURAL IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_naturaln(p_in IN NATURALN, p_in_out IN OUT NATURALN, p_out OUT NATURALN) RETURN NATURALN IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_positive(p_in IN POSITIVE, p_in_out IN OUT POSITIVE, p_out OUT POSITIVE) RETURN POSITIVE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_positiven(p_in IN POSITIVEN, p_in_out IN OUT POSITIVEN, p_out OUT POSITIVEN) RETURN POSITIVEN IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_float(p_in IN FLOAT, p_in_out IN OUT FLOAT, p_out OUT FLOAT) RETURN FLOAT IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;	

    FUNCTION func_binary_float(p_in IN BINARY_FLOAT, p_in_out IN OUT BINARY_FLOAT, p_out OUT BINARY_FLOAT) RETURN BINARY_FLOAT IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_real(p_in IN REAL, p_in_out IN OUT REAL, p_out OUT REAL) RETURN REAL IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;	

    PROCEDURE proc_binary_float_const(p_min_normal OUT BINARY_FLOAT, p_max_normal OUT BINARY_FLOAT) IS
	BEGIN
		SELECT	BINARY_FLOAT_MIN_NORMAL, BINARY_FLOAT_MAX_NORMAL
		INTO		p_min_normal, p_max_normal
		FROM		DUAL;

		--package_log.insert_log_info ('odpt_pkg_main.proc_binary_float_const',    -- source
			--'BINARY_FLOAT_MIN_NORMAL: ' || p_min_normal
			--|| ' BINARY_FLOAT_MAX_NORMAL: ' || p_max_normal
			--|| ' BINARY_FLOAT_MIN_SUBNORMAL: ' || p_min_subnormal
			--|| ' BINARY_FLOAT_MAX_SUBNORMAL: ' || p_max_subnormal, 
			--'ODPT', -- user
			--NULL);		
		RETURN;
	END;

    FUNCTION func_binary_double(p_in IN BINARY_DOUBLE, p_in_out IN OUT BINARY_DOUBLE, p_out OUT BINARY_DOUBLE) RETURN BINARY_DOUBLE IS
		l_ret		NUMBER;
	BEGIN	
		--package_log.insert_log_info ('odpt_pkg_main.func_binary_double', 'Start of function', 'ODPT', NULL);
--		l_ret := odpt_pkg_table_big.insert_row(
--					NULL,	NULL,	NULL, NULL, NULL,	NULL,	NULL,
--					p_in,
--					NULL, NULL,	NULL,	NULL,	NULL,	NULL, NULL, NULL, NULL,	NULL, NULL, NULL, NULL);				

		p_in_out := p_in;
		p_out := p_in;
		--package_log.insert_log_info ('odpt_pkg_main.func_binary_double', 'End of function', 'ODPT', NULL);
		RETURN p_in;
	END;

    PROCEDURE proc_binary_double_const(p_min_normal OUT BINARY_DOUBLE, p_max_normal OUT BINARY_DOUBLE) IS
	BEGIN
		SELECT	BINARY_DOUBLE_MIN_NORMAL, BINARY_DOUBLE_MAX_NORMAL
		INTO		p_min_normal, p_max_normal
		FROM		DUAL;

--		package_log.insert_log_info ('odpt_pkg_main.proc_binary_double_const',    -- source
--			'BINARY_DOUBLE_MIN_NORMAL: ' || p_min_normal
--			|| ' BINARY_DOUBLE_MAX_NORMAL: ' || p_max_normal,
--			'ODPT', -- user
--			NULL);		
		RETURN;
	END;

    FUNCTION func_double_precision(p_in IN DOUBLE PRECISION, p_in_out IN OUT DOUBLE PRECISION, p_out OUT DOUBLE PRECISION) RETURN DOUBLE PRECISION IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_rowid(p_in IN ROWID, p_in_out IN OUT ROWID, p_out OUT ROWID)	RETURN ROWID IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_urowid(p_in IN UROWID, p_in_out IN OUT UROWID, p_out OUT UROWID) RETURN UROWID IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_long(p_in IN LONG, p_in_out IN OUT LONG, p_out OUT LONG) RETURN LONG IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_date(p_in IN DATE, p_in_out IN OUT DATE, p_out OUT DATE) RETURN DATE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_timestamp(p_in IN TIMESTAMP, p_in_out IN OUT TIMESTAMP, p_out OUT TIMESTAMP)	RETURN TIMESTAMP IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_timestamp_w_l_time_zone(p_in IN TIMESTAMP WITH LOCAL TIME ZONE, p_in_out IN OUT TIMESTAMP WITH LOCAL TIME ZONE, p_out OUT TIMESTAMP WITH LOCAL TIME ZONE) RETURN TIMESTAMP WITH LOCAL TIME ZONE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_timestamp_w_time_zone(p_in IN TIMESTAMP WITH TIME ZONE, p_in_out IN OUT TIMESTAMP WITH TIME ZONE, p_out OUT TIMESTAMP WITH TIME ZONE) RETURN TIMESTAMP WITH TIME ZONE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_long_raw(p_in IN LONG RAW, p_in_out IN OUT LONG RAW, p_out OUT LONG RAW) RETURN LONG RAW IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_raw(p_in IN RAW, p_in_out IN OUT RAW, p_out OUT RAW) RETURN RAW IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_bfile(p_in IN BFILE, p_in_out IN OUT BFILE, p_out OUT BFILE) RETURN BFILE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_blob(p_in IN BLOB, p_in_out IN OUT BLOB, p_out OUT BLOB) RETURN BLOB IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_boolean(p_in IN BOOLEAN, p_in_out IN OUT BOOLEAN, p_out OUT BOOLEAN) RETURN BOOLEAN IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_clob(p_in IN CLOB, p_in_out IN OUT CLOB, p_out OUT CLOB) RETURN CLOB IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_nclob(p_in IN NCLOB, p_in_out IN OUT NCLOB, p_out OUT NCLOB) RETURN NCLOB IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_xmltype(p_in IN XMLTYPE, p_in_out IN OUT XMLTYPE, p_out OUT XMLTYPE) RETURN XMLTYPE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_cursor_ignore_aa_integer RETURN t_cursor_ignore_aa_integer IS
	BEGIN
		RETURN NULL;
	END;

	FUNCTION func_cursor_ignore_boolean RETURN t_cursor_ignore_boolean IS
	BEGIN
		RETURN NULL;
	END;
    
	FUNCTION func_cursor_ignore_rowid RETURN t_cursor_ignore_rowid IS
	BEGIN
		RETURN NULL;
	END;
    
	FUNCTION func_cursor_ignore_urowid RETURN t_cursor_ignore_urowid IS
	BEGIN
		RETURN NULL;
	END;
  
	FUNCTION func_cursor_ignore_raw RETURN t_cursor_ignore_raw IS
	BEGIN
		RETURN NULL;
	END;
    
	FUNCTION func_cursor_ignore_bfile RETURN t_cursor_ignore_bfile IS
	BEGIN
		RETURN NULL;
	END;
    
	FUNCTION func_cursor_ignore_xmltype RETURN t_cursor_ignore_xmltype IS
	BEGIN
		RETURN NULL;
	END;
    
	FUNCTION func_cursor_ignore_long RETURN t_cursor_ignore_long IS
	BEGIN
		RETURN NULL;
	END;
    
	FUNCTION func_cursor_ignore_long_raw RETURN t_cursor_ignore_long_raw IS
	BEGIN
		RETURN NULL;
	END;

/*   FUNCTION func_cursor_typed(p_out OUT t_cursor_typed_big) RETURN t_cursor_typed2 IS
	BEGIN
		p_out := NULL;
		RETURN NULL;
	END;

    FUNCTION func_cursor_untyped(p_out OUT t_cursor_untyped) RETURN t_cursor_untyped IS
	BEGIN
		p_out := NULL;
		RETURN NULL;
	END;

    FUNCTION func_cursor_typed_numbers(p_out OUT t_cursor_typed_numbers) RETURN t_cursor_typed2 IS
	BEGIN
		p_out := NULL;
		RETURN NULL;
	END; */

	FUNCTION func_object_type(p_in IN odpt_big_ot, p_in_out IN OUT odpt_big_ot, p_out OUT odpt_big_ot) RETURN odpt_big_ot IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_record(p_in IN odpt_pkg_table_big.t_table_big, p_in_out IN OUT odpt_pkg_table_big.t_table_big, p_out OUT odpt_pkg_table_big.t_table_big) RETURN odpt_pkg_table_big.t_table_big IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_row(p_in IN odpt_table_big%ROWTYPE, p_in_out IN OUT odpt_table_big%ROWTYPE, p_out OUT odpt_table_big%ROWTYPE) RETURN odpt_table_big%ROWTYPE IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_interval_day_to_second(p_in IN INTERVAL DAY TO SECOND, p_in_out IN OUT INTERVAL DAY TO SECOND, p_out OUT INTERVAL DAY TO SECOND) RETURN INTERVAL DAY TO SECOND IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_interval_year_to_month(p_in IN INTERVAL YEAR TO MONTH, p_in_out IN OUT INTERVAL YEAR TO MONTH, p_out OUT INTERVAL YEAR TO MONTH) RETURN INTERVAL YEAR TO MONTH IS
	BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

   ---------------
    FUNCTION func_aa_integer (p_in IN t_assocarray_integer, p_in_out IN OUT t_assocarray_integer, p_out OUT t_assocarray_integer) RETURN t_assocarray_integer IS
    BEGIN
	   	p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_binary_integer (p_in IN t_assocarray_binary_integer, p_in_out IN OUT t_assocarray_binary_integer, p_out OUT t_assocarray_binary_integer) RETURN t_assocarray_binary_integer IS
    BEGIN
		--package_log.insert_log_info ('odpt_pkg_main.func_binary_integer', 'Start of function', 'ODPT', NULL);
		p_in_out := p_in;
		p_out := p_in;
		--package_log.insert_log_info ('odpt_pkg_main.func_binary_integer', 'End of function', 'ODPT', NULL);
		RETURN p_in;
	END;

    FUNCTION func_aa_pls_integer (p_in IN t_assocarray_pls_integer, p_in_out IN OUT t_assocarray_pls_integer, p_out OUT t_assocarray_pls_integer) RETURN t_assocarray_pls_integer IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_natural (p_in IN t_assocarray_natural, p_in_out IN OUT t_assocarray_natural, p_out OUT t_assocarray_natural) RETURN t_assocarray_natural IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_naturaln (p_in IN t_assocarray_naturaln, p_in_out IN OUT t_assocarray_naturaln, p_out OUT t_assocarray_naturaln) RETURN t_assocarray_naturaln IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_positive (p_in IN t_assocarray_positive, p_in_out IN OUT t_assocarray_positive, p_out OUT t_assocarray_positive) RETURN t_assocarray_positive IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_positiven (p_in IN t_assocarray_positiven, p_in_out IN OUT t_assocarray_positiven, p_out OUT t_assocarray_positiven) RETURN t_assocarray_positiven IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_boolean (p_in IN t_assocarray_boolean, p_in_out IN OUT t_assocarray_boolean, p_out OUT t_assocarray_boolean) RETURN t_assocarray_boolean IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_aa_record (p_in IN t_assocarray_record, p_in_out IN OUT t_assocarray_record, p_out OUT t_assocarray_record) RETURN t_assocarray_record IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	FUNCTION func_aa_row (p_in IN t_assocarray_row, p_in_out IN OUT t_assocarray_row, p_out OUT t_assocarray_row) RETURN t_assocarray_row IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_smallint (p_in IN t_assocarray_smallint, p_in_out IN OUT t_assocarray_smallint, p_out OUT t_assocarray_smallint) RETURN t_assocarray_smallint IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_number (p_in IN t_assocarray_number, p_in_out IN OUT t_assocarray_number, p_out OUT t_assocarray_number) RETURN t_assocarray_number IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_binary_double (p_in IN t_assocarray_binary_double, p_in_out IN OUT t_assocarray_binary_double, p_out OUT t_assocarray_binary_double) RETURN t_assocarray_binary_double IS
		l_ret		NUMBER;
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		--l_idx := p_in.NEXT;
--		l_ret := odpt_pkg_table_big.insert_row(
--					NULL,	NULL,	NULL, NULL, NULL,	NULL,	NULL,
--					p_in(1),
--					NULL, NULL,	NULL,	NULL,	NULL,	NULL, NULL, NULL, NULL,	NULL, NULL, NULL, NULL
--		);				
--		l_ret := odpt_pkg_table_big.insert_row(
--					NULL,	NULL,	NULL, NULL, NULL,	NULL,	NULL,
--					p_in(2),
--					NULL, NULL,	NULL,	NULL,	NULL,	NULL, NULL, NULL, NULL,	NULL, NULL, NULL, NULL
--		);				
--		COMMIT;
		RETURN p_in;
	END;

    FUNCTION func_aa_binary_float (p_in IN t_assocarray_binary_float, p_in_out IN OUT t_assocarray_binary_float, p_out OUT t_assocarray_binary_float) RETURN t_assocarray_binary_float IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_double_precision (p_in IN t_assocarray_double_precision, p_in_out IN OUT t_assocarray_double_precision, p_out OUT t_assocarray_double_precision) RETURN t_assocarray_double_precision IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_numeric (p_in IN t_assocarray_numeric, p_in_out IN OUT t_assocarray_numeric, p_out OUT t_assocarray_numeric) RETURN t_assocarray_numeric IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_decimal (p_in IN t_assocarray_decimal, p_in_out IN OUT t_assocarray_decimal, p_out OUT t_assocarray_decimal) RETURN t_assocarray_decimal IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

   FUNCTION func_aa_float (p_in IN t_assocarray_float, p_in_out IN OUT t_assocarray_float, p_out OUT t_assocarray_float) RETURN t_assocarray_float IS
   BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_real (p_in IN t_assocarray_real, p_in_out IN OUT t_assocarray_real, p_out OUT t_assocarray_real) RETURN t_assocarray_real IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

   FUNCTION func_aa_varchar2 (p_in IN t_assocarray_varchar2, p_in_out IN OUT t_assocarray_varchar2, p_out OUT t_assocarray_varchar2) RETURN t_assocarray_varchar2 IS
		l_idx		INTEGER;
   BEGIN
		p_in_out := p_in;
		p_out := p_in;

--		l_idx := p_in.FIRST;
--		WHILE l_idx IS NOT NULL LOOP
--			p_in_out(l_idx) := p_in(l_idx);
--			p_out(l_idx) := p_in(l_idx);
--			l_idx := p_in.NEXT(l_idx);
--		END LOOP;

		RETURN p_in;
	END;

    FUNCTION func_aa_nvarchar2 (p_in IN t_assocarray_nvarchar2, p_in_out IN OUT t_assocarray_nvarchar2, p_out OUT t_assocarray_nvarchar2) RETURN t_assocarray_nvarchar2 IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_nchar (p_in IN t_assocarray_nchar, p_in_out IN OUT t_assocarray_nchar, p_out OUT t_assocarray_nchar) RETURN t_assocarray_nchar IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_char (p_in IN t_assocarray_char, p_in_out IN OUT t_assocarray_char, p_out OUT t_assocarray_char) RETURN t_assocarray_char IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_string (p_in IN t_assocarray_string, p_in_out IN OUT t_assocarray_string, p_out OUT t_assocarray_string) RETURN t_assocarray_string IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;	

    FUNCTION func_aa_date (p_in IN t_assocarray_date, p_in_out IN OUT t_assocarray_date, p_out OUT t_assocarray_date) RETURN t_assocarray_date IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_timestamp (p_in IN t_assocarray_timestamp, p_in_out IN OUT t_assocarray_timestamp, p_out OUT t_assocarray_timestamp) RETURN t_assocarray_timestamp IS
    BEGIN
		--package_log.insert_log_info ('odpt_pkg_main.func_aa_timestamp', 'Start of function', 'ODPT', NULL);
		p_in_out := p_in;
		p_out := p_in;
		--package_log.insert_log_info ('odpt_pkg_main.func_aa_timestamp', 'End of function', 'ODPT', NULL);
		RETURN p_in;
	END;

    FUNCTION func_aa_timestamp_prec0 (p_in IN t_assocarray_timestamp_prec0, p_in_out IN OUT t_assocarray_timestamp_prec0, p_out OUT t_assocarray_timestamp_prec0) RETURN t_assocarray_timestamp_prec0 IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_timestamp_prec9 (p_in IN t_assocarray_timestamp_prec9, p_in_out IN OUT t_assocarray_timestamp_prec9, p_out OUT t_assocarray_timestamp_prec9) RETURN t_assocarray_timestamp_prec9 IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_blob (p_in IN t_assocarray_blob, p_in_out IN OUT t_assocarray_blob, p_out OUT t_assocarray_blob) RETURN t_assocarray_blob IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_clob (p_in IN t_assocarray_clob, p_in_out IN OUT t_assocarray_clob, p_out OUT t_assocarray_clob) RETURN t_assocarray_clob IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

    FUNCTION func_aa_nclob (p_in IN t_assocarray_nclob, p_in_out IN OUT t_assocarray_nclob, p_out OUT t_assocarray_nclob) RETURN t_assocarray_nclob IS
    BEGIN
		p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

   ---------------
    FUNCTION func_aa_integer_v (p_in IN t_assocarray_integer_v, p_in_out IN OUT t_assocarray_integer_v, p_out OUT t_assocarray_integer_v) RETURN t_assocarray_integer_v IS
    BEGIN
	   	p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	-----------------------
	FUNCTION func_nt_number (p_in IN t_nestedtable_number, p_in_out IN OUT t_nestedtable_number, p_out OUT t_nestedtable_number) RETURN t_nestedtable_number IS
    BEGIN
	   	p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

	----------------------
	FUNCTION func_va_number (p_in IN t_va_number, p_in_out IN OUT t_va_number, p_out OUT t_va_number) RETURN t_va_number IS
    BEGIN
	   	p_in_out := p_in;
		p_out := p_in;
		RETURN p_in;
	END;

END odpt_pkg_main;
/