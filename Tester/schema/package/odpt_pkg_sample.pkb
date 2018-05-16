CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_sample AS
                                     
    FUNCTION get_rows_typed_ret (p_in_number IN NUMBER, p_in_out_varchar2 IN OUT VARCHAR2, p_in_out_assocarray_integer IN OUT t_assocarray_integer, 
        p_out_date OUT DATE) RETURN t_ref_cursor_table_big_partial IS
        l_cursor    t_ref_cursor_table_big_partial;
        l_idx		INTEGER;        
    BEGIN
        OPEN l_cursor FOR
        SELECT      id, col_integer, col_number, col_varchar2_max, col_date, col_timestamp
        FROM        odpt_table_big
        ORDER BY    id;    

        -- multiply each value in assoc array by 7 
        l_idx := p_in_out_assocarray_integer.FIRST;
        WHILE l_idx IS NOT NULL LOOP
            p_in_out_assocarray_integer(l_idx) := p_in_out_assocarray_integer(l_idx) * 7;
            l_idx := p_in_out_assocarray_integer.NEXT(l_idx);
        END LOOP;
        
        p_in_out_varchar2 := 'Goodbye';        
        p_out_date := TO_DATE ('31-DEC-1999');        
        RETURN l_cursor;
    END;	

    FUNCTION get_rows_untyped_ret (p_in_integer IN INTEGER) RETURN t_ref_cursor IS
        l_cursor    t_ref_cursor;
    BEGIN    
        OPEN l_cursor FOR
        SELECT      id, col_integer, col_number, col_varchar2_max, col_date, col_timestamp
        FROM        odpt_table_big
        ORDER BY    id;    

        RETURN l_cursor;
    END;	
    
END odpt_pkg_sample;
/
