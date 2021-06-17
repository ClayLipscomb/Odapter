CREATE OR REPLACE PACKAGE ODPT.fltr_pkg_rec_used AS

	-- untyped cursor type
	TYPE t_ref_cursor IS REF CURSOR;

	-- typed cursor type
	TYPE t_table_big_flt_used IS RECORD
      (		id odpt_table_big.id%TYPE,
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
            col_timestamp_t_z					odpt_table_big.col_timestamp_t_z%TYPE,
            col_timestamp_t_z_prec0				odpt_table_big.col_timestamp_t_z_prec0%TYPE,
            col_timestamp_t_z_prec9				odpt_table_big.col_timestamp_t_z_prec9%TYPE,
            col_timestamp_l_t_z					odpt_table_big.col_timestamp_l_t_z%TYPE,
            col_timestamp_l_t_z_prec0			odpt_table_big.col_timestamp_l_t_z_prec0%TYPE,
            col_timestamp_l_t_z_prec9			odpt_table_big.col_timestamp_l_t_z_prec9%TYPE,
	
			col_blob odpt_table_big.col_blob%TYPE,
			col_clob odpt_table_big.col_clob%TYPE,
			col_nclob odpt_table_big.col_nclob%TYPE,

			-- C# keywords
			abstract							odpt_table_big.abstract%TYPE,
			base								odpt_table_big.base%TYPE,
			void								odpt_table_big.void%TYPE,
			class								odpt_table_big.class%TYPE,
			namespace 							odpt_table_big.namespace%TYPE,
			readonly 							odpt_table_big.readonly%TYPE,
			partial  							odpt_table_big.partial%TYPE,
			const   							odpt_table_big.const%TYPE,
			using   							odpt_table_big.using%TYPE,

			col_last odpt_table_big.col_last%TYPE);
	TYPE t_ref_cursor_table_big IS REF CURSOR RETURN t_table_big_flt_used;

	FUNCTION get_rows_typed_ret RETURN t_ref_cursor_table_big;

END fltr_pkg_rec_used;
/