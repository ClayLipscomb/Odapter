CREATE OR REPLACE PACKAGE ODPT.odpt_pkg_table_big AS

	-- typed cursor type
   TYPE t_table_big IS RECORD
      (  id odpt_table_big.id%TYPE,
			col_number_id odpt_table_big.col_number_id%TYPE,
			col_integer odpt_table_big.col_integer%TYPE,
			col_int odpt_table_big.col_int%TYPE,
			col_smallint odpt_table_big.col_smallint%TYPE,
			col_numeric odpt_table_big.col_numeric%TYPE,
			col_decimal odpt_table_big.col_decimal%TYPE,
			col_number odpt_table_big.col_number%TYPE,
			col_double_precision odpt_table_big.col_double_precision%TYPE,
			col_float odpt_table_big.col_float%TYPE,
			col_real odpt_table_big.col_real%TYPE,
			col_binary_float odpt_table_big.col_binary_float%TYPE,
			col_binary_double odpt_table_big.col_binary_double%TYPE,
								
			col_varchar_min odpt_table_big.col_varchar_min%TYPE,
			col_varchar_max odpt_table_big.col_varchar_max%TYPE,
			col_varchar2_min odpt_table_big.col_varchar2_min%TYPE,
			col_varchar2_max odpt_table_big.col_varchar2_max%TYPE,
			col_nvarchar2_min odpt_table_big.col_nvarchar2_min%TYPE,
			col_nvarchar2_max odpt_table_big.col_nvarchar2_max%TYPE,
			
			col_char_min odpt_table_big.col_char_min%TYPE,
			col_char_max odpt_table_big.col_char_max%TYPE,
			col_nchar_min odpt_table_big.col_nchar_min%TYPE,
			col_nchar_max odpt_table_big.col_nchar_max%TYPE,

			col_date odpt_table_big.col_date%TYPE,
			col_timestamp odpt_table_big.col_timestamp%TYPE,
			col_timestamp_prec0 odpt_table_big.col_timestamp_prec0%TYPE,
			col_timestamp_prec9 odpt_table_big.col_timestamp_prec9%TYPE,

			col_blob odpt_table_big.col_blob%TYPE,
			col_clob odpt_table_big.col_clob%TYPE,
			col_nclob odpt_table_big.col_nclob%TYPE,

			col_last odpt_table_big.col_last%TYPE);
	TYPE t_ref_cursor_table_big IS REF CURSOR RETURN t_table_big;

	TYPE t_table_big_char IS RECORD
      (  	id odpt_table_big.id%TYPE,
			col_varchar_min odpt_table_big.col_varchar_min%TYPE,
			col_varchar_max odpt_table_big.col_varchar_max%TYPE,
			col_varchar2_min odpt_table_big.col_varchar2_min%TYPE,
			col_varchar2_max odpt_table_big.col_varchar2_max%TYPE,
			col_nvarchar2_min odpt_table_big.col_nvarchar2_min%TYPE,
			col_nvarchar2_max odpt_table_big.col_nvarchar2_max%TYPE,
			col_char_min odpt_table_big.col_char_min%TYPE,
			col_char_max odpt_table_big.col_char_max%TYPE,
			col_nchar_min odpt_table_big.col_nchar_min%TYPE,
			col_nchar_max odpt_table_big.col_nchar_max%TYPE,

			col_last odpt_table_big.col_last%TYPE);
	TYPE t_ref_cursor_table_big_char IS REF CURSOR RETURN t_table_big_char;
	
	-- untyped cursor type
	TYPE t_ref_cursor IS REF CURSOR;
  
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

							p_col_blob IN odpt_table_big.col_blob%TYPE,
							p_col_clob IN odpt_table_big.col_clob%TYPE,
							p_col_nclob IN odpt_table_big.col_nclob%TYPE
							) RETURN INTEGER;
								
	FUNCTION insert_row (p_row IN odpt_table_big%ROWTYPE) RETURN INTEGER;
	PROCEDURE delete_row(p_id IN odpt_table_big.id%TYPE); 
	PROCEDURE trunc_table;
	PROCEDURE proc_typed_cursor_in(p_ref_cursor IN t_ref_cursor_table_big);
	PROCEDURE proc_untyped_cursor_in(p_ref_cursor IN t_ref_cursor);	
	PROCEDURE proc_typed_cursor_in_out(p_ref_cursor IN OUT t_ref_cursor_table_big);
	PROCEDURE proc_untyped_cursor_in_out(p_ref_cursor IN OUT t_ref_cursor);	
	
	FUNCTION get_rows_typed_fltr_unused(p_ref_cursor OUT fltr_pkg_rec_unused.t_ref_cursor_table_big, p_ref_cursor2 OUT fltr_pkg_rec_unused.t_ref_cursor_table_big) RETURN fltr_pkg_rec_unused.t_ref_cursor_table_big;
	FUNCTION get_rows_untyped_fltr_unused(p_ref_cursor OUT fltr_pkg_rec_unused.t_ref_cursor, p_ref_cursor2 OUT fltr_pkg_rec_unused.t_ref_cursor) RETURN fltr_pkg_rec_unused.t_ref_cursor;

	FUNCTION get_rows_typed_fltr_used(p_ref_cursor OUT fltr_pkg_rec_used.t_ref_cursor_table_big, p_ref_cursor2 OUT fltr_pkg_rec_used.t_ref_cursor_table_big) RETURN fltr_pkg_rec_used.t_ref_cursor_table_big;
	FUNCTION get_rows_untyped_fltr_used(p_ref_cursor OUT fltr_pkg_rec_used.t_ref_cursor, p_ref_cursor2 OUT fltr_pkg_rec_used.t_ref_cursor) RETURN fltr_pkg_rec_used.t_ref_cursor;
	
	FUNCTION get_rows_typed_ret RETURN t_ref_cursor_table_big;
	FUNCTION get_rows_typed_out(p_ref_cursor OUT t_ref_cursor_table_big) RETURN INTEGER;	
	FUNCTION get_rows_typed_out_ret(p_ref_cursor OUT t_ref_cursor_table_big) RETURN t_ref_cursor_table_big;	
	FUNCTION get_rows_typed_out2(p_ref_cursor OUT t_ref_cursor_table_big, p_ref_cursor2 OUT t_ref_cursor_table_big_char) RETURN INTEGER;
	FUNCTION get_rows_typed_out2_ret(p_ref_cursor OUT t_ref_cursor_table_big, p_ref_cursor2 OUT t_ref_cursor_table_big_char) RETURN t_ref_cursor_table_big;

	FUNCTION get_rows_untyped_ret RETURN t_ref_cursor;	
	FUNCTION get_rows_untyped_out(p_ref_cursor OUT t_ref_cursor) RETURN INTEGER;	
	FUNCTION get_rows_untyped_out_ret(p_ref_cursor OUT t_ref_cursor) RETURN t_ref_cursor;	
	FUNCTION get_rows_untyped_out2(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN INTEGER;	
	FUNCTION get_rows_untyped_out2_ret(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN t_ref_cursor;	

END odpt_pkg_table_big;
/