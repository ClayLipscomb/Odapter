CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_table_big AS
                                     
  FUNCTION insert_row (	p_col_number_id IN odpt_table_big.col_number_id%TYPE,
								p_col_integer IN odpt_table_big.col_integer%TYPE,
								p_col_int IN odpt_table_big.col_int%TYPE,
								p_col_smallint IN odpt_table_big.col_smallint%TYPE,
								p_col_numeric IN odpt_table_big.col_numeric%TYPE,
								p_col_decimal IN odpt_table_big.col_decimal%TYPE,
								p_col_number IN odpt_table_big.col_number%TYPE,
								p_col_double_precision IN odpt_table_big.col_double_precision%TYPE,

								p_col_float IN odpt_table_big.col_float%TYPE,
								p_col_real IN odpt_table_big.col_real%TYPE,
								p_col_binary_float IN odpt_table_big.col_binary_float%TYPE,
								p_col_binary_double IN odpt_table_big.col_binary_double%TYPE,
								p_col_varchar_min IN odpt_table_big.col_varchar_min%TYPE,
								p_col_varchar_max IN odpt_table_big.col_varchar_max%TYPE,
								p_col_varchar2_min IN odpt_table_big.col_varchar2_min%TYPE,
								p_col_varchar2_max IN odpt_table_big.col_varchar2_max%TYPE,
								p_col_nvarchar2_min IN odpt_table_big.col_nvarchar2_min%TYPE,
								p_col_nvarchar2_max IN odpt_table_big.col_nvarchar2_max%TYPE,

								p_col_char_min IN odpt_table_big.col_char_min%TYPE,
								p_col_char_max IN odpt_table_big.col_char_max%TYPE,
								p_col_nchar_min IN odpt_table_big.col_nchar_min%TYPE,
								p_col_nchar_max IN odpt_table_big.col_nchar_max%TYPE,

								p_col_date IN odpt_table_big.col_date%TYPE,
								p_col_timestamp IN odpt_table_big.col_timestamp%TYPE,
								p_col_timestamp_prec0 IN odpt_table_big.col_timestamp_prec0%TYPE,								
								p_col_timestamp_prec9 IN odpt_table_big.col_timestamp_prec9%TYPE,
                                p_col_timestamp_t_z					IN odpt_table_big.col_timestamp_t_z%TYPE,
                                p_col_timestamp_t_z_prec0			IN odpt_table_big.col_timestamp_t_z_prec0%TYPE,
                                p_col_timestamp_t_z_prec9			IN odpt_table_big.col_timestamp_t_z_prec9%TYPE,
                                p_col_timestamp_l_t_z				IN odpt_table_big.col_timestamp_l_t_z%TYPE,
                                p_col_timestamp_l_t_z_prec0			IN odpt_table_big.col_timestamp_l_t_z_prec0%TYPE,
                                p_col_timestamp_l_t_z_prec9			IN odpt_table_big.col_timestamp_l_t_z_prec9%TYPE,
								
								p_col_blob IN odpt_table_big.col_blob%TYPE,
								p_col_clob IN odpt_table_big.col_clob%TYPE,
								p_col_nclob IN odpt_table_big.col_nclob%TYPE,
                                
                    			-- C# keywords
                                abstract							IN odpt_table_big.abstract%TYPE,
                                base								IN odpt_table_big.base%TYPE,
                                void								IN odpt_table_big.void%TYPE,
                                class								IN odpt_table_big.class%TYPE,
                                namespace 							IN odpt_table_big.namespace%TYPE,
                                readonly 							IN odpt_table_big.readonly%TYPE,
                                partial  							IN odpt_table_big.partial%TYPE,
                                const   							IN odpt_table_big.const%TYPE,
                                using   							IN odpt_table_big.using%TYPE                             
								) RETURN INTEGER IS
      l_id			INTEGER;
   BEGIN
      -- get primary key    
      SELECT odpt_big_s.NEXTVAL INTO l_id FROM dual;
 
      INSERT INTO odpt_table_big (id,
					col_number_id,
					col_integer,
					col_int,
					col_smallint,
					col_numeric,
					col_decimal,
					col_number,
					col_double_precision,
					col_float,
					col_real,
					col_binary_float,
					col_binary_double,
					col_varchar_min,
					col_varchar_max,
					col_varchar2_min,
					col_varchar2_max,
					col_nvarchar2_min,
					col_nvarchar2_max,
					col_char_min,
					col_char_max,
					col_nchar_min,
					col_nchar_max,
					col_date,
					col_timestamp,
					col_timestamp_prec0,
					col_timestamp_prec9,
                    col_timestamp_t_z,
                    col_timestamp_t_z_prec0,
                    col_timestamp_t_z_prec9,
                    col_timestamp_l_t_z,
                    col_timestamp_l_t_z_prec0,
                    col_timestamp_l_t_z_prec9,
					col_blob,
					col_clob,
					col_nclob,
                    
                    abstract,
                    base,
	                void,
    				class,
					namespace,
					readonly,
					partial,
					const,
                    using,
                    
					col_last)
      VALUES (	l_id,
					p_col_number_id,
					p_col_integer,
					p_col_int,
					p_col_smallint,
					p_col_numeric,
					p_col_decimal,
					p_col_number,
					p_col_double_precision,
					p_col_float,
					p_col_real,
					p_col_binary_float,
					p_col_binary_double,
					p_col_varchar_min,
					p_col_varchar_max,
					p_col_varchar2_min,
					p_col_varchar2_max,
					p_col_nvarchar2_min,
					p_col_nvarchar2_max,
					p_col_char_min,
					p_col_char_max,
					p_col_nchar_min,
					p_col_nchar_max,
					p_col_date,
					p_col_timestamp,
					p_col_timestamp_prec0,
					p_col_timestamp_prec9,
                    p_col_timestamp_t_z,
                    p_col_timestamp_t_z_prec0,
                    p_col_timestamp_t_z_prec9,
                    p_col_timestamp_l_t_z,
                    p_col_timestamp_l_t_z_prec0,
                    p_col_timestamp_l_t_z_prec9,
					p_col_blob,
					p_col_clob,
					p_col_nclob,
                    
                    abstract,
                    base,
	                void,
    				class,
					namespace,
					readonly,
					partial,
					const,
                    using,
                    
					NULL);

		RETURN l_id;
	END;

	FUNCTION insert_row (p_row IN odpt_table_big%ROWTYPE) RETURN INTEGER IS
	BEGIN	
		RETURN insert_row(
					p_row.col_number_id,
					p_row.col_integer,
					p_row.col_int,
					p_row.col_smallint,
					p_row.col_numeric,
					p_row.col_decimal,
					p_row.col_number,
					p_row.col_double_precision,
					p_row.col_float,
					p_row.col_real,
					p_row.col_binary_float,
					p_row.col_binary_double,
					p_row.col_varchar_min,
					p_row.col_varchar_max,
					p_row.col_varchar2_min,
					p_row.col_varchar2_max,
					p_row.col_nvarchar2_min,
					p_row.col_nvarchar2_max,
					p_row.col_char_min,
					p_row.col_char_max,
					p_row.col_nchar_min,
					p_row.col_nchar_max,
					p_row.col_date,
					p_row.col_timestamp,
					p_row.col_timestamp_prec0,
					p_row.col_timestamp_prec9,
                    p_row.col_timestamp_t_z,
                    p_row.col_timestamp_t_z_prec0,
                    p_row.col_timestamp_t_z_prec9,
                    p_row.col_timestamp_l_t_z,
                    p_row.col_timestamp_l_t_z_prec0,
                    p_row.col_timestamp_l_t_z_prec9,
					p_row.col_blob,
					p_row.col_clob,
					p_row.col_nclob,
                    
                    p_row.abstract,
                    p_row.base,
	                p_row.void,
    				p_row.class,
					p_row.namespace,
					p_row.readonly,
					p_row.partial,
					p_row.const,
                    p_row.using
		);		
	END;

	PROCEDURE delete_row(p_id IN odpt_table_big.id%TYPE) IS 
	BEGIN
		DELETE 
		FROM	odpt_table_big
		WHERE	id = p_id;
   END;

	PROCEDURE trunc_table IS 
	BEGIN
		EXECUTE IMMEDIATE 'TRUNCATE TABLE odpt_table_big';
		odpt_pkg_log.insert_log_info ('odpt_pkg_table_big.trunc_table', 'End of function', 'ODPT', NULL);
   END;

	PROCEDURE proc_typed_cursor_in(p_ref_cursor IN t_ref_cursor_table_big) IS
	BEGIN
		NULL;
	END;

	PROCEDURE proc_untyped_cursor_in(p_ref_cursor IN t_ref_cursor) IS
	BEGIN
		NULL;
	END;

	PROCEDURE proc_typed_cursor_in_out(p_ref_cursor IN OUT t_ref_cursor_table_big) IS
	BEGIN
		NULL;
	END;

	PROCEDURE proc_untyped_cursor_in_out(p_ref_cursor IN OUT t_ref_cursor) IS
	BEGIN
		NULL;
	END;

	--------------------------------------------------
	-- cursors and record types from filtered packages
	FUNCTION get_rows_typed_fltr_unused(p_ref_cursor OUT fltr_pkg_rec_unused.t_ref_cursor_table_big, p_ref_cursor2 OUT fltr_pkg_rec_unused.t_ref_cursor_table_big) RETURN fltr_pkg_rec_unused.t_ref_cursor_table_big IS
		l_cursor	fltr_pkg_rec_unused.t_ref_cursor_table_big;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_untyped_fltr_unused(p_ref_cursor OUT fltr_pkg_rec_unused.t_ref_cursor, p_ref_cursor2 OUT fltr_pkg_rec_unused.t_ref_cursor) RETURN fltr_pkg_rec_unused.t_ref_cursor IS
		l_cursor	fltr_pkg_rec_unused.t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;		

	FUNCTION get_rows_typed_fltr_used(p_ref_cursor OUT fltr_pkg_rec_used.t_ref_cursor_table_big, p_ref_cursor2 OUT fltr_pkg_rec_used.t_ref_cursor_table_big) RETURN fltr_pkg_rec_used.t_ref_cursor_table_big IS
		l_cursor	fltr_pkg_rec_used.t_ref_cursor_table_big;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_untyped_fltr_used(p_ref_cursor OUT fltr_pkg_rec_used.t_ref_cursor, p_ref_cursor2 OUT fltr_pkg_rec_used.t_ref_cursor) RETURN fltr_pkg_rec_used.t_ref_cursor IS
		l_cursor	fltr_pkg_rec_used.t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	----------------
	-- typed cursors
	FUNCTION get_rows_typed_table_row_type RETURN t_ref_cursor_table_row_type IS
		l_cursor	t_ref_cursor_table_row_type;
    BEGIN
        RETURN l_cursor;
    END;
    
	FUNCTION get_rows_typed_ret RETURN t_ref_cursor_table_big IS
		l_cursor	t_ref_cursor_table_big;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_typed_out(p_ref_cursor OUT t_ref_cursor_table_big) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN 0;
	END;	

	FUNCTION get_rows_typed_out_ret(p_ref_cursor OUT t_ref_cursor_table_big) RETURN t_ref_cursor_table_big IS	
		l_cursor	t_ref_cursor_table_big;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_typed_out2(p_ref_cursor OUT t_ref_cursor_table_big, p_ref_cursor2 OUT t_ref_cursor_table_big_char) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_varchar_min, col_varchar_max, col_varchar2_min, col_varchar2_max, col_nvarchar2_min, col_nvarchar2_max, col_char_min, col_char_max, col_nchar_min, col_nchar_max, col_last		
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN 0;
	END;

	FUNCTION get_rows_typed_out2_ret(p_ref_cursor OUT t_ref_cursor_table_big, p_ref_cursor2 OUT t_ref_cursor_table_big_char) RETURN t_ref_cursor_table_big IS
		l_cursor	t_ref_cursor_table_big;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_varchar_min, col_varchar_max, col_varchar2_min, col_varchar2_max, col_nvarchar2_min, col_nvarchar2_max, col_char_min, col_char_max, col_nchar_min, col_nchar_max, col_last		
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;		

	------------------
	-- untyped cursors
	FUNCTION get_rows_untyped_ret RETURN t_ref_cursor IS
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_untyped_out(p_ref_cursor OUT t_ref_cursor) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN 0;
	END;	

	FUNCTION get_rows_untyped_out_ret(p_ref_cursor OUT t_ref_cursor) RETURN t_ref_cursor IS
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;		

	FUNCTION get_rows_untyped_out2(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_varchar_min, col_varchar_max, col_varchar2_min, col_varchar2_max, col_nvarchar2_min, col_nvarchar2_max, col_char_min, col_char_max, col_nchar_min, col_nchar_max, col_last		
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN 0;
	END;

	FUNCTION get_rows_untyped_out2_ret(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN t_ref_cursor IS
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_varchar_min, col_varchar_max, col_varchar2_min, col_varchar2_max, col_nvarchar2_min, col_nvarchar2_max, col_char_min, col_char_max, col_nchar_min, col_nchar_max, col_last		
		FROM  odpt_table_big
		ORDER BY id;    

		RETURN l_cursor;
	END;		
END odpt_pkg_table_big;
/