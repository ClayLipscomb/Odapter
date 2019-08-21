CREATE OR REPLACE PACKAGE BODY ODPT.fltr_pkg_rec_used AS
	
	FUNCTION get_rows_typed_ret RETURN t_ref_cursor_table_big IS
		l_cursor	t_ref_cursor_table_big;
	BEGIN
		OPEN l_cursor FOR
		SELECT *
		FROM  odpt_table_big
		ORDER BY id;    
	
		RETURN l_cursor;
	END;	
		
END fltr_pkg_rec_used;
/