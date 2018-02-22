CREATE OR REPLACE PACKAGE ODPT.odpt_pkg_table_number AS

	-- typed cursor types
   TYPE t_table_number IS RECORD
      (  id 							odpt_table_number.id%TYPE,
			col_number					odpt_table_number.col_number%TYPE,
			col_number_1				odpt_table_number.col_number_1%TYPE,
			col_number_2				odpt_table_number.col_number_2%TYPE,
			col_number_3				odpt_table_number.col_number_3%TYPE,
			col_number_4				odpt_table_number.col_number_4%TYPE,
			col_number_5				odpt_table_number.col_number_5%TYPE,
			col_number_6				odpt_table_number.col_number_6%TYPE,
			col_number_7				odpt_table_number.col_number_7%TYPE,
			col_number_8				odpt_table_number.col_number_8%TYPE,
			col_number_9				odpt_table_number.col_number_9%TYPE,
			col_number_10				odpt_table_number.col_number_10%TYPE,
			--col_number_11				odpt_table_number.col_number_11%TYPE,
			--col_number_12				odpt_table_number.col_number_12%TYPE,
			--col_number_13				odpt_table_number.col_number_13%TYPE,
			--col_number_14				odpt_table_number.col_number_14%TYPE,
			--col_number_15				odpt_table_number.col_number_15%TYPE,
			--col_number_16				odpt_table_number.col_number_16%TYPE,
			--col_number_17				odpt_table_number.col_number_17%TYPE,
			col_number_18				odpt_table_number.col_number_18%TYPE,
			col_number_19				odpt_table_number.col_number_19%TYPE,
			--col_number_20				odpt_table_number.col_number_20%TYPE,
			--col_number_21				odpt_table_number.col_number_21%TYPE,
			--col_number_22				odpt_table_number.col_number_22%TYPE,
			--col_number_23				odpt_table_number.col_number_23%TYPE,
			--col_number_24				odpt_table_number.col_number_24%TYPE,
			--col_number_25				odpt_table_number.col_number_25%TYPE,
			--col_number_26				odpt_table_number.col_number_26%TYPE,
			--col_number_27				odpt_table_number.col_number_27%TYPE,
			--col_number_28				odpt_table_number.col_number_28%TYPE,
			--col_number_29				odpt_table_number.col_number_29%TYPE,
			--col_number_30				odpt_table_number.col_number_30%TYPE,
			--col_number_31				odpt_table_number.col_number_31%TYPE,
			--col_number_32				odpt_table_number.col_number_32%TYPE,
			--col_number_33				odpt_table_number.col_number_33%TYPE,
			--col_number_34				odpt_table_number.col_number_34%TYPE,
			--col_number_35				odpt_table_number.col_number_35%TYPE,
			--col_number_36				odpt_table_number.col_number_36%TYPE,
			--col_number_37				odpt_table_number.col_number_37%TYPE,
			col_number_38				odpt_table_number.col_number_38%TYPE,
			col_number_1_scale_0		odpt_table_number.col_number_1_scale_0%TYPE,
			col_number_2_scale_0		odpt_table_number.col_number_2_scale_0%TYPE,
			col_number_3_scale_0		odpt_table_number.col_number_3_scale_0%TYPE,
			col_number_4_scale_0		odpt_table_number.col_number_4_scale_0%TYPE,
			col_number_5_scale_0		odpt_table_number.col_number_5_scale_0%TYPE,
			col_number_9_scale_0		odpt_table_number.col_number_9_scale_0%TYPE,
			col_number_10_scale_0	odpt_table_number.col_number_10_scale_0%TYPE,
			col_number_18_scale_0	odpt_table_number.col_number_18_scale_0%TYPE,
			col_number_19_scale_0	odpt_table_number.col_number_19_scale_0%TYPE,
			col_number_28_scale_0	odpt_table_number.col_number_28_scale_0%TYPE,
			col_number_29_scale_0	odpt_table_number.col_number_29_scale_0%TYPE,
			col_number_38_scale_0	odpt_table_number.col_number_38_scale_0%TYPE,
			col_number_2_scale_1		odpt_table_number.col_number_2_scale_1%TYPE,
			col_number_5_scale_3		odpt_table_number.col_number_5_scale_3%TYPE,
			col_number_15_scale_11	odpt_table_number.col_number_15_scale_11%TYPE,
			col_number_31_scale_21	odpt_table_number.col_number_31_scale_21%TYPE,
			col_number_38_scale_37	odpt_table_number.col_number_38_scale_37%TYPE,
			col_number_last			odpt_table_number.col_number_last%TYPE		);
	TYPE t_ref_cursor_table_number IS REF CURSOR RETURN t_table_number;

   TYPE t_table_number_dec IS RECORD
      ( 	id 							odpt_table_number.id%TYPE,
			col_number					odpt_table_number.col_number%TYPE,
			col_number_2_scale_1		odpt_table_number.col_number_2_scale_1%TYPE,
			col_number_5_scale_3		odpt_table_number.col_number_5_scale_3%TYPE,
			col_number_15_scale_11	odpt_table_number.col_number_15_scale_11%TYPE,
			col_number_31_scale_21	odpt_table_number.col_number_31_scale_21%TYPE,
			col_number_38_scale_37	odpt_table_number.col_number_38_scale_37%TYPE,
			col_number_last			odpt_table_number.col_number_last%TYPE		);
	TYPE t_ref_cursor_table_number_dec IS REF CURSOR RETURN t_table_number_dec;

	-- untyped cursor type
	TYPE t_ref_cursor IS REF CURSOR;
  
	FUNCTION insert_row (	p_col_number					IN odpt_table_number.col_number%TYPE,
			p_col_number_1					IN odpt_table_number.col_number_1%TYPE,
			p_col_number_2					IN odpt_table_number.col_number_2%TYPE,
			p_col_number_3					IN odpt_table_number.col_number_3%TYPE,
			p_col_number_4					IN odpt_table_number.col_number_4%TYPE,
			p_col_number_5					IN odpt_table_number.col_number_5%TYPE,
			p_col_number_6					IN odpt_table_number.col_number_6%TYPE,
			p_col_number_7					IN odpt_table_number.col_number_7%TYPE,
			p_col_number_8					IN odpt_table_number.col_number_8%TYPE,
			p_col_number_9					IN odpt_table_number.col_number_9%TYPE,
			p_col_number_10				IN odpt_table_number.col_number_10%TYPE,
			--p_col_number_11				IN odpt_table_number.col_number_11%TYPE,
			--p_col_number_12				IN odpt_table_number.col_number_12%TYPE,
			--p_col_number_13				IN odpt_table_number.col_number_13%TYPE,
			--p_col_number_14				IN odpt_table_number.col_number_14%TYPE,
			--p_col_number_15				IN odpt_table_number.col_number_15%TYPE,
			--p_col_number_16				IN odpt_table_number.col_number_16%TYPE,
			--p_col_number_17				IN odpt_table_number.col_number_17%TYPE,
			p_col_number_18				IN odpt_table_number.col_number_18%TYPE,
			p_col_number_19				IN odpt_table_number.col_number_19%TYPE,
			--p_col_number_20				IN odpt_table_number.col_number_20%TYPE,
			--p_col_number_21				IN odpt_table_number.col_number_21%TYPE,
			--p_col_number_22				IN odpt_table_number.col_number_22%TYPE,
			--p_col_number_23				IN odpt_table_number.col_number_23%TYPE,
			--p_col_number_24				IN odpt_table_number.col_number_24%TYPE,
			--p_col_number_25				IN odpt_table_number.col_number_25%TYPE,
			--p_col_number_26				IN odpt_table_number.col_number_26%TYPE,
			--p_col_number_27				IN odpt_table_number.col_number_27%TYPE,
			--p_col_number_28				IN odpt_table_number.col_number_28%TYPE,
			--p_col_number_29				IN odpt_table_number.col_number_29%TYPE,
			--p_col_number_30				IN odpt_table_number.col_number_30%TYPE,
			--p_col_number_31				IN odpt_table_number.col_number_31%TYPE,
			--p_col_number_32				IN odpt_table_number.col_number_32%TYPE,
			--p_col_number_33				IN odpt_table_number.col_number_33%TYPE,
			--p_col_number_34				IN odpt_table_number.col_number_34%TYPE,
			--p_col_number_35				IN odpt_table_number.col_number_35%TYPE,
			--p_col_number_36				IN odpt_table_number.col_number_36%TYPE,
			--p_col_number_37				IN odpt_table_number.col_number_37%TYPE,
			p_col_number_38				IN odpt_table_number.col_number_38%TYPE,
			p_col_number_1_scale_0		IN odpt_table_number.col_number_1_scale_0%TYPE,
			p_col_number_2_scale_0		IN odpt_table_number.col_number_2_scale_0%TYPE,
			p_col_number_3_scale_0		IN odpt_table_number.col_number_3_scale_0%TYPE,
			p_col_number_4_scale_0		IN odpt_table_number.col_number_4_scale_0%TYPE,
			p_col_number_5_scale_0		IN odpt_table_number.col_number_5_scale_0%TYPE,
			p_col_number_9_scale_0		IN odpt_table_number.col_number_9_scale_0%TYPE,
			p_col_number_10_scale_0		IN odpt_table_number.col_number_10_scale_0%TYPE,
			p_col_number_18_scale_0		IN odpt_table_number.col_number_18_scale_0%TYPE,
			p_col_number_19_scale_0		IN odpt_table_number.col_number_19_scale_0%TYPE,
			p_col_number_28_scale_0		IN odpt_table_number.col_number_28_scale_0%TYPE,
			p_col_number_29_scale_0		IN odpt_table_number.col_number_29_scale_0%TYPE,
			p_col_number_38_scale_0		IN odpt_table_number.col_number_38_scale_0%TYPE,
			p_col_number_2_scale_1		IN odpt_table_number.col_number_2_scale_1%TYPE,
			p_col_number_5_scale_3		IN odpt_table_number.col_number_5_scale_3%TYPE,
			p_col_number_15_scale_11	IN odpt_table_number.col_number_15_scale_11%TYPE,
			p_col_number_31_scale_21	IN odpt_table_number.col_number_31_scale_21%TYPE,
			p_col_number_38_scale_37	IN odpt_table_number.col_number_38_scale_37%TYPE,
			p_col_number_last				IN odpt_table_number.col_number_last%TYPE  ) RETURN INTEGER;
								
	FUNCTION insert_row (p_row IN odpt_table_number%ROWTYPE) RETURN INTEGER;
	PROCEDURE delete_row (p_id IN odpt_table_number.id%TYPE); 
	PROCEDURE trunc_table;
	
	FUNCTION get_rows_typed_ret RETURN t_ref_cursor_table_number;
	FUNCTION get_rows_typed_out(p_ref_cursor OUT t_ref_cursor_table_number) RETURN INTEGER;
	FUNCTION get_rows_typed_out_ret(p_ref_cursor OUT t_ref_cursor_table_number) RETURN t_ref_cursor_table_number;
	FUNCTION get_rows_typed_out2(p_ref_cursor OUT t_ref_cursor_table_number, p_ref_cursor2 OUT t_ref_cursor_table_number_dec) RETURN INTEGER;
	FUNCTION get_rows_typed_out2_ret(p_ref_cursor OUT t_ref_cursor_table_number, p_ref_cursor2 OUT t_ref_cursor_table_number_dec) RETURN t_ref_cursor_table_number;
	
	FUNCTION get_rows_untyped_ret RETURN t_ref_cursor;	
	FUNCTION get_rows_untyped_out(p_ref_cursor OUT t_ref_cursor) RETURN INTEGER;	
	FUNCTION get_rows_untyped_out_ret(p_ref_cursor OUT t_ref_cursor) RETURN t_ref_cursor;
	FUNCTION get_rows_untyped_out2(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN INTEGER;
	FUNCTION get_rows_untyped_out2_ret(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN t_ref_cursor;

END odpt_pkg_table_number;
/