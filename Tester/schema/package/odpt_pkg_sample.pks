CREATE OR REPLACE PACKAGE ODPT.odpt_pkg_sample AS

    -- strongly typed cursor
    TYPE t_table_big_partial IS RECORD (
        id                  odpt_table_big.id%TYPE,                 -- NUMBER
        col_integer         odpt_table_big.col_integer%TYPE,        -- INTEGER
        col_number          odpt_table_big.col_number%TYPE,         -- NUMBER
        col_varchar2_max    odpt_table_big.col_varchar2_max%TYPE,   -- VARCHAR2(4000)
        col_date            odpt_table_big.col_date%TYPE,           -- DATE
        col_timestamp       odpt_table_big.col_timestamp%TYPE);     -- TIMESTAMP
    TYPE t_ref_cursor_table_big_partial IS REF CURSOR RETURN t_table_big_partial;
	
    FUNCTION get_rows_typed_ret (p_in_number IN NUMBER, p_in_out_varchar2 IN OUT VARCHAR2, p_out_date OUT DATE) RETURN t_ref_cursor_table_big_partial;

END odpt_pkg_sample;
/