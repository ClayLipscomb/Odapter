CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_table_number AS
                                     
  FUNCTION insert_row (	
			p_col_number					IN odpt_table_number.col_number%TYPE,
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
			p_col_number_last				IN odpt_table_number.col_number_last%TYPE ) RETURN INTEGER IS
		l_id			INTEGER;
   BEGIN
      -- get primary key   
      SELECT odpt_number_s.NEXTVAL INTO l_id FROM dual;
      
      INSERT INTO odpt_table_number (	
			id									,
			col_number						,
			col_number_1					,
			col_number_2					,
			col_number_3					,
			col_number_4					,
			col_number_5					,
			col_number_6					,
			col_number_7					,
			col_number_8					,
			col_number_9					,
			col_number_10					,
			--col_number_11					,
			--col_number_12					,
			--col_number_13					,
			--col_number_14					,
			--col_number_15					,
			--col_number_16					,
			--col_number_17					,
			col_number_18					,
			col_number_19					,
			--col_number_20					,
			--col_number_21					,
			--col_number_22					,
			--col_number_23					,
			--col_number_24					,
			--col_number_25					,
			--col_number_26					,
			--col_number_27					,
			--col_number_28					,
			--col_number_29					,
			--col_number_30					,
			--col_number_31					,
			--col_number_32					,
			--col_number_33					,
			--col_number_34					,
			--col_number_35					,
			--col_number_36					,
			--col_number_37					,
			col_number_38					, 
			col_number_1_scale_0			,
			col_number_2_scale_0			,
			col_number_3_scale_0			,
			col_number_4_scale_0			,
			col_number_5_scale_0			,
			col_number_9_scale_0			,
			col_number_10_scale_0		,
			col_number_18_scale_0		,
			col_number_19_scale_0		,
			col_number_28_scale_0		,
			col_number_29_scale_0		,
			col_number_38_scale_0		,
			col_number_2_scale_1			,
			col_number_5_scale_3			,
			col_number_15_scale_11		,
			col_number_31_scale_21		,
			col_number_38_scale_37		,
			col_number_last			)
      VALUES (	l_id,
			p_col_number						,
			p_col_number_1					,
			p_col_number_2					,
			p_col_number_3					,
			p_col_number_4					,
			p_col_number_5					,
			p_col_number_6					,
			p_col_number_7					,
			p_col_number_8					,
			p_col_number_9					,
			p_col_number_10					,
			--p_col_number_11					,
			--p_col_number_12					,
			--p_col_number_13					,
			--p_col_number_14					,
			--p_col_number_15					,
			--p_col_number_16					,
			--p_col_number_17					,
			p_col_number_18					,
			p_col_number_19					,
			--p_col_number_20					,
			--p_col_number_21					,
			--p_col_number_22					,
			--p_col_number_23					,
			--p_col_number_24					,
			--p_col_number_25					,
			--p_col_number_26					,
			--p_col_number_27					,
			--p_col_number_28					,
			--p_col_number_29					,
			--p_col_number_30					,
			--p_col_number_31					,
			--p_col_number_32					,
			--p_col_number_33					,
			--p_col_number_34					,
			--p_col_number_35					,
			--p_col_number_36					,
			--p_col_number_37					,
			p_col_number_38					, 
			p_col_number_1_scale_0			,
			p_col_number_2_scale_0			,
			p_col_number_3_scale_0			,
			p_col_number_4_scale_0			,
			p_col_number_5_scale_0			,
			p_col_number_9_scale_0			,
			p_col_number_10_scale_0		,
			p_col_number_18_scale_0		,
			p_col_number_19_scale_0		,
			p_col_number_28_scale_0		,
			p_col_number_29_scale_0		,
			p_col_number_38_scale_0		,
			p_col_number_2_scale_1			,
			p_col_number_5_scale_3			,
			p_col_number_15_scale_11		,
			p_col_number_31_scale_21		,
			p_col_number_38_scale_37		,
			NULL);

		RETURN l_id;
	END;

	FUNCTION insert_row (p_row IN odpt_table_number%ROWTYPE) RETURN INTEGER IS
	BEGIN	
		RETURN insert_row(
					p_row.col_number						,
					p_row.col_number_1					,
					p_row.col_number_2					,
					p_row.col_number_3					,
					p_row.col_number_4					,
					p_row.col_number_5					,
					p_row.col_number_6					,
					p_row.col_number_7					,
					p_row.col_number_8					,
					p_row.col_number_9					,
					p_row.col_number_10					,
					--p_row.col_number_11					,
					--p_row.col_number_12					,
					--p_row.col_number_13					,
					--p_row.col_number_14					,
					--p_row.col_number_15					,
					--p_row.col_number_16					,
					--p_row.col_number_17					,
					p_row.col_number_18					,
					p_row.col_number_19					,
					--p_row.col_number_20					,
					--p_row.col_number_21					,
					--p_row.col_number_22					,
					--p_row.col_number_23					,
					--p_row.col_number_24					,
					--p_row.col_number_25					,
					--p_row.col_number_26					,
					--p_row.col_number_27					,
					--p_row.col_number_28					,
					--p_row.col_number_29					,
					--p_row.col_number_30					,
					--p_row.col_number_31					,
					--p_row.col_number_32					,
					--p_row.col_number_33					,
					--p_row.col_number_34					,
					--p_row.col_number_35					,
					--p_row.col_number_36					,
					--p_row.col_number_37					,
					p_row.col_number_38					, 
					p_row.col_number_1_scale_0			,
					p_row.col_number_2_scale_0			,
					p_row.col_number_3_scale_0			,
					p_row.col_number_4_scale_0			,
					p_row.col_number_5_scale_0			,
					p_row.col_number_9_scale_0			,
					p_row.col_number_10_scale_0		,
					p_row.col_number_18_scale_0		,
					p_row.col_number_19_scale_0		,
					p_row.col_number_28_scale_0		,
					p_row.col_number_29_scale_0		,
					p_row.col_number_38_scale_0		,
					p_row.col_number_2_scale_1			,
					p_row.col_number_5_scale_3			,
					p_row.col_number_15_scale_11		,
					p_row.col_number_31_scale_21		,
					p_row.col_number_38_scale_37		,
					p_row.col_number_last					);		
	END;
	
	PROCEDURE delete_row(p_id IN odpt_table_number.id%TYPE) IS 
	BEGIN
		DELETE 
		FROM	odpt_table_number
		WHERE	id = p_id;
   END;

	PROCEDURE trunc_table IS 
	BEGIN
		EXECUTE IMMEDIATE 'TRUNCATE TABLE odpt_table_number';
		odpt_pkg_log.insert_log_info ('odpt_pkg_table_number.trunc_table', 'End of function', 'ODPT', NULL);
   END;
	
	----------------
	-- typed cursors
	FUNCTION get_rows_typed_ret RETURN t_ref_cursor_table_number IS
		l_cursor	t_ref_cursor_table_number;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    
	
		RETURN l_cursor;
	END;	

	FUNCTION get_rows_typed_out(p_ref_cursor OUT t_ref_cursor_table_number) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    
		RETURN 0;
	END;	

	FUNCTION get_rows_typed_out_ret(p_ref_cursor OUT t_ref_cursor_table_number) RETURN t_ref_cursor_table_number IS	
		l_cursor	t_ref_cursor_table_number;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_typed_out2(p_ref_cursor OUT t_ref_cursor_table_number, p_ref_cursor2 OUT t_ref_cursor_table_number_dec) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_number, col_number_2_scale_1, col_number_5_scale_3, col_number_15_scale_11, col_number_31_scale_21, col_number_38_scale_37, col_number_last			
		FROM  odpt_table_number
		ORDER BY id;    
		
		RETURN 0;
	END;

	FUNCTION get_rows_typed_out2_ret(p_ref_cursor OUT t_ref_cursor_table_number, p_ref_cursor2 OUT t_ref_cursor_table_number_dec) RETURN t_ref_cursor_table_number IS
		l_cursor	t_ref_cursor_table_number;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_number, col_number_2_scale_1, col_number_5_scale_3, col_number_15_scale_11, col_number_31_scale_21, col_number_38_scale_37, col_number_last			
		FROM  odpt_table_number
		ORDER BY id;    
		
		RETURN l_cursor;
	END;		
	
	----------------
	-- untyped cursors
	FUNCTION get_rows_untyped_ret RETURN t_ref_cursor IS
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    
		
		RETURN l_cursor;
	END;	
   
	FUNCTION get_rows_untyped_out(p_ref_cursor OUT t_ref_cursor) RETURN INTEGER IS
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    
		
		p_ref_cursor := l_cursor;
		RETURN 0;
	END;	

	FUNCTION get_rows_untyped_out_ret(p_ref_cursor OUT t_ref_cursor) RETURN t_ref_cursor IS	
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		RETURN l_cursor;
	END;	

	FUNCTION get_rows_untyped_out2(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN INTEGER IS
	BEGIN
		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_number, col_number_2_scale_1, col_number_5_scale_3, col_number_15_scale_11, col_number_31_scale_21, col_number_38_scale_37, col_number_last			
		FROM  odpt_table_number
		ORDER BY id;    
		
		RETURN 0;
	END;

	FUNCTION get_rows_untyped_out2_ret(p_ref_cursor OUT t_ref_cursor, p_ref_cursor2 OUT t_ref_cursor) RETURN t_ref_cursor IS
		l_cursor	t_ref_cursor;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor FOR
		SELECT *
		FROM  odpt_table_number
		ORDER BY id;    

		OPEN p_ref_cursor2 FOR
		SELECT id, col_number, col_number_2_scale_1, col_number_5_scale_3, col_number_15_scale_11, col_number_31_scale_21, col_number_38_scale_37, col_number_last			
		FROM  odpt_table_number
		ORDER BY id;    
		
		RETURN l_cursor;
	END;		

END odpt_pkg_table_number;
/
