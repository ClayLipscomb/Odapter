CREATE OR REPLACE TYPE ODPT.odpt_base_sc_trans_type_ot AS OBJECT (

   flexible_column1        VARCHAR2(250),
   flexible_column2        VARCHAR2(250),
   flexible_column3        VARCHAR2(250),
   flexible_column4        VARCHAR2(250),
   flexible_column5        VARCHAR2(250),
   
   flexible_column6        VARCHAR2(1000),
   flexible_column7        VARCHAR2(1000),
   flexible_column8        VARCHAR2(1000),
   flexible_column9        VARCHAR2(1000),
   flexible_column10       VARCHAR2(1000),

   flexible_column11       VARCHAR2(4000),
   flexible_column12       VARCHAR2(4000),
   flexible_column13       VARCHAR2(4000),
   flexible_column14       VARCHAR2(4000),
   flexible_column15       NVARCHAR2(2000),
      
   flexible_column16       CHAR,
   flexible_column17       CLOB,
   flexible_column18       DATE,   
   flexible_column19       DATE,
   flexible_column20       TIMESTAMP,   
   
   flexible_column21       NUMBER,
   flexible_column22       NUMBER,
   flexible_column23       NUMBER,
   flexible_column24       NUMBER,
   flexible_column25       NUMBER,
  
   FINAL MEMBER PROCEDURE init_flexible_data(
      flexible_column1  VARCHAR2,   flexible_column2  VARCHAR2,   flexible_column3  VARCHAR2,   flexible_column4  VARCHAR2,   flexible_column5  VARCHAR2,
      flexible_column6  VARCHAR2,   flexible_column7  VARCHAR2,   flexible_column8  VARCHAR2,   flexible_column9  VARCHAR2,   flexible_column10 VARCHAR2,
      flexible_column11 VARCHAR2,   flexible_column12 VARCHAR2,   flexible_column13 VARCHAR2,   flexible_column14 VARCHAR2,   flexible_column15 NVARCHAR2,
      flexible_column16 CHAR,       flexible_column17 CLOB,       flexible_column18 DATE,       flexible_column19 DATE,       flexible_column20 TIMESTAMP,
      flexible_column21 NUMBER,     flexible_column22 NUMBER,     flexible_column23 NUMBER,     flexible_column24 NUMBER,     flexible_column25 NUMBER),
  
   --------------------------------------------------------
   -- determines whether object has changed compared to earlier
   --    version of object by comparing each attribute
   --------------------------------------------------------
   NOT FINAL NOT INSTANTIABLE MEMBER FUNCTION has_same_state(p_base_object odpt_base_sc_trans_type_ot) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION has_same_state_flexible(p_base_object odpt_base_sc_trans_type_ot) RETURN BOOLEAN,  

   --------------------------------------------------------
   -- return PK Id value based on subtype-specific columns
   --------------------------------------------------------
   NOT FINAL NOT INSTANTIABLE MEMBER FUNCTION get_pk_id RETURN VARCHAR2,   

   --------------------------------------------------------
   -- type specific compare methods
   --------------------------------------------------------
   FINAL MEMBER FUNCTION equal_varchar2(p_val1 VARCHAR2, p_val2 VARCHAR2) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_nvarchar2(p_val1 NVARCHAR2, p_val2 NVARCHAR2) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_char(p_val1 CHAR, p_val2 CHAR) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_number(p_val1 NUMBER, p_val2 NUMBER) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_date(p_val1 DATE, p_val2 DATE) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_timestamp(p_val1 TIMESTAMP, p_val2 TIMESTAMP) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_blob(p_val1 BLOB, p_val2 BLOB) RETURN BOOLEAN,  
   FINAL MEMBER FUNCTION equal_clob(p_val1 CLOB, p_val2 CLOB) RETURN BOOLEAN,  
  
   --------------------------------------------------------
   -- build unique string id based on provided columns
   --------------------------------------------------------
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2) RETURN VARCHAR2,
  
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2) RETURN VARCHAR2,
  
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2) RETURN VARCHAR2,
  
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2) RETURN VARCHAR2,
  
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                      col5 VARCHAR2) RETURN VARCHAR2,  
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                      col5 VARCHAR2, col6 VARCHAR2) RETURN VARCHAR2, 
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                      col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2) RETURN VARCHAR2, 
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                      col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2, col8 VARCHAR2) RETURN VARCHAR2, 
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                      col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2, col8 VARCHAR2,
                                      col9 VARCHAR2) RETURN VARCHAR2, 
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                      col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2, col8 VARCHAR2,
                                      col9 VARCHAR2, col10 VARCHAR2) RETURN VARCHAR2
  
) NOT INSTANTIABLE NOT FINAL;
/