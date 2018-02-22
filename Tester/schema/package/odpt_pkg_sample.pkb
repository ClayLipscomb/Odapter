CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_sample AS
                                     
    FUNCTION get_rows_typed_ret (p_in_number IN NUMBER, p_in_out_varchar2 IN OUT VARCHAR2, p_out_date OUT DATE) RETURN t_ref_cursor_table_big_partial IS
        l_cursor    t_ref_cursor_table_big_partial;
    BEGIN
        OPEN l_cursor FOR
        SELECT      id, col_integer, col_number, col_varchar2_max, col_date, col_timestamp
        FROM        odpt_table_big
        ORDER BY    id;    

        p_in_out_varchar2 := 'Goodbye';        
        p_out_date := TO_DATE ('31-DEC-1999');        
        RETURN l_cursor;
    END;	

END odpt_pkg_sample;
/
