CREATE OR REPLACE TYPE BODY ODPT.odpt_base_sc_trans_type_ot AS

   --------------------------------------------------------
   -- Initialize all generic attributes defined on this 
   --   object type. This allows subtypes to simply call
   --   this method.
   --------------------------------------------------------
   
   FINAL MEMBER PROCEDURE init_flexible_data(
      flexible_column1  VARCHAR2,   flexible_column2  VARCHAR2,   flexible_column3  VARCHAR2,   flexible_column4  VARCHAR2,   flexible_column5  VARCHAR2,
      flexible_column6  VARCHAR2,   flexible_column7  VARCHAR2,   flexible_column8  VARCHAR2,   flexible_column9  VARCHAR2,   flexible_column10 VARCHAR2,
      flexible_column11 VARCHAR2,   flexible_column12 VARCHAR2,   flexible_column13 VARCHAR2,   flexible_column14 VARCHAR2,   flexible_column15 NVARCHAR2,
      flexible_column16 CHAR,       flexible_column17 CLOB,       flexible_column18 DATE,       flexible_column19 DATE,       flexible_column20 TIMESTAMP,
      flexible_column21 NUMBER,     flexible_column22 NUMBER,     flexible_column23 NUMBER,     flexible_column24 NUMBER,     flexible_column25 NUMBER) IS
   BEGIN
      
      SELF.flexible_column1  := flexible_column1;
      SELF.flexible_column2  := flexible_column2;
      SELF.flexible_column3  := flexible_column3;
      SELF.flexible_column4  := flexible_column4;
      SELF.flexible_column5  := flexible_column5;
      SELF.flexible_column6  := flexible_column6;
      SELF.flexible_column7  := flexible_column7;
      SELF.flexible_column8  := flexible_column8;
      SELF.flexible_column9  := flexible_column9;
      SELF.flexible_column10 := flexible_column10;
      SELF.flexible_column11 := flexible_column11;
      SELF.flexible_column12 := flexible_column12;
      SELF.flexible_column13 := flexible_column13;
      SELF.flexible_column14 := flexible_column14;
      SELF.flexible_column15 := flexible_column15;
      SELF.flexible_column16 := flexible_column16;
      SELF.flexible_column17 := flexible_column17;
      SELF.flexible_column18 := flexible_column18;
      SELF.flexible_column19 := flexible_column19;
      SELF.flexible_column20 := flexible_column20;
      SELF.flexible_column21 := flexible_column21;
      SELF.flexible_column22 := flexible_column22;
      SELF.flexible_column23 := flexible_column23;
      SELF.flexible_column24 := flexible_column24;
      SELF.flexible_column25 := flexible_column25;
      RETURN;
   END;

   -------------------------------------------------------------
   -- determines whether object has changed compared to earlier
   --    version of object by comparing each attribute
   -------------------------------------------------------------
   FINAL MEMBER FUNCTION has_same_state_flexible(p_base_object odpt_base_sc_trans_type_ot) RETURN BOOLEAN IS
   BEGIN
      IF p_base_object IS NULL THEN
         RETURN FALSE;
      END IF;
      
      --dbms_output.put_line('base_type_ot.has_same_state_generic(p_base_object base_type_ot) called');    
      RETURN (
             SELF.equal_varchar2(SELF.flexible_column1,   p_base_object.flexible_column1)
         AND SELF.equal_varchar2(SELF.flexible_column2,   p_base_object.flexible_column2)
         AND SELF.equal_varchar2(SELF.flexible_column3,   p_base_object.flexible_column3)
         AND SELF.equal_varchar2(SELF.flexible_column4,   p_base_object.flexible_column4)
         AND SELF.equal_varchar2(SELF.flexible_column5,   p_base_object.flexible_column5)
         AND SELF.equal_varchar2(SELF.flexible_column6,   p_base_object.flexible_column6)
         AND SELF.equal_varchar2(SELF.flexible_column7,   p_base_object.flexible_column7)
         AND SELF.equal_varchar2(SELF.flexible_column8,   p_base_object.flexible_column8)
         AND SELF.equal_varchar2(SELF.flexible_column9,   p_base_object.flexible_column9)
         AND SELF.equal_varchar2(SELF.flexible_column10,  p_base_object.flexible_column10)
         AND SELF.equal_varchar2(SELF.flexible_column11,  p_base_object.flexible_column11)
         AND SELF.equal_varchar2(SELF.flexible_column12,  p_base_object.flexible_column12)
         AND SELF.equal_varchar2(SELF.flexible_column13,  p_base_object.flexible_column13)
         AND SELF.equal_varchar2(SELF.flexible_column14,  p_base_object.flexible_column14)         
         AND SELF.equal_nvarchar2(SELF.flexible_column15, p_base_object.flexible_column15)
         
         AND SELF.equal_char(SELF.flexible_column16, p_base_object.flexible_column16)
         AND SELF.equal_clob(SELF.flexible_column17, p_base_object.flexible_column17)         
         AND SELF.equal_date(SELF.flexible_column18, p_base_object.flexible_column18)           
         AND SELF.equal_date(SELF.flexible_column19, p_base_object.flexible_column19)
         AND SELF.equal_timestamp(SELF.flexible_column20, p_base_object.flexible_column20)
         
         AND SELF.equal_number(SELF.flexible_column21, p_base_object.flexible_column21)
         AND SELF.equal_number(SELF.flexible_column22, p_base_object.flexible_column22)
         AND SELF.equal_number(SELF.flexible_column23, p_base_object.flexible_column23)
         AND SELF.equal_number(SELF.flexible_column24, p_base_object.flexible_column24)
         AND SELF.equal_number(SELF.flexible_column25, p_base_object.flexible_column25)
        );        
   END has_same_state_flexible;

   --------------------------------------------------------
   -- type specific compare methods
   --------------------------------------------------------
   FINAL MEMBER FUNCTION equal_varchar2(p_val1 VARCHAR2, p_val2 VARCHAR2) RETURN BOOLEAN IS
   BEGIN
      RETURN (CASE WHEN ( (p_val1 = p_val2) OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;    
    
   FINAL MEMBER FUNCTION equal_nvarchar2(p_val1 NVARCHAR2, p_val2 NVARCHAR2) RETURN BOOLEAN IS
   BEGIN
      RETURN (CASE WHEN ( (p_val1 = p_val2) OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;
       
   FINAL MEMBER FUNCTION equal_char(p_val1 CHAR, p_val2 CHAR) RETURN BOOLEAN IS
   BEGIN
      RETURN (CASE WHEN ( (p_val1 = p_val2) OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;    
    
   FINAL MEMBER FUNCTION equal_number(p_val1 NUMBER, p_val2 NUMBER) RETURN BOOLEAN IS
   BEGIN    
      RETURN (CASE WHEN ( (p_val1 = p_val2) OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;
        
   FINAL MEMBER FUNCTION equal_date(p_val1 DATE, p_val2 DATE) RETURN BOOLEAN IS     
   BEGIN
      RETURN (CASE WHEN ( (p_val1 = p_val2) OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;    

   FINAL MEMBER FUNCTION equal_timestamp(p_val1 TIMESTAMP, p_val2 TIMESTAMP) RETURN BOOLEAN IS     
   BEGIN
      RETURN (CASE WHEN ( (p_val1 = p_val2) OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;    

   FINAL MEMBER FUNCTION equal_blob(p_val1 BLOB, p_val2 BLOB) RETURN BOOLEAN IS     
   BEGIN
      RETURN (CASE WHEN ( dbms_lob.compare(p_val1, p_val2) = 0 OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;    

   FINAL MEMBER FUNCTION equal_clob(p_val1 CLOB, p_val2 CLOB) RETURN BOOLEAN IS     
   BEGIN
      RETURN (CASE WHEN ( dbms_lob.compare(p_val1, p_val2) = 0 OR (p_val1 IS NULL AND p_val2 IS NULL) ) THEN TRUE ELSE FALSE END);
   END;    

   --------------------------------------------------------
   -- build unique string id based on provided columns
   --------------------------------------------------------
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);        
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, NULL, NULL, NULL, NULL, NULL, NULL, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, col4, NULL, NULL, NULL, NULL, NULL, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                     col5 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, col4, col5, NULL, NULL, NULL, NULL, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                     col5 VARCHAR2, col6 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, col4, col5, col6, NULL, NULL, NULL, NULL); 
   END create_pk_id;
   
   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                     col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, col4, col5, col6, col7, NULL, NULL, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                     col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2, col8 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, col4, col5, col6, col7, col8, NULL, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                     col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2, col8 VARCHAR2,
                                     col9 VARCHAR2) RETURN VARCHAR2 IS
   BEGIN
      RETURN create_pk_id(col1, col2, col3, col4, col5, col6, col7, col8, col9, NULL); 
   END create_pk_id;

   FINAL MEMBER FUNCTION create_pk_id(col1 VARCHAR2, col2 VARCHAR2, col3 VARCHAR2, col4 VARCHAR2, 
                                     col5 VARCHAR2, col6 VARCHAR2, col7 VARCHAR2, col8 VARCHAR2,
                                     col9 VARCHAR2, col10 VARCHAR2) RETURN VARCHAR2 IS
      delimit CHAR := '^';
   BEGIN
      RETURN CASE WHEN col1 IS NULL THEN 'NULL' ELSE UPPER(col1) END 
          || CASE WHEN col2 IS NULL THEN ''        ELSE delimit || UPPER(col2) END
          || CASE WHEN col3 IS NULL THEN ''        ELSE delimit || UPPER(col3) END
          || CASE WHEN col4 IS NULL THEN ''        ELSE delimit || UPPER(col4) END
          || CASE WHEN col5 IS NULL THEN ''        ELSE delimit || UPPER(col5) END
          || CASE WHEN col6 IS NULL THEN ''        ELSE delimit || UPPER(col6) END
          || CASE WHEN col7 IS NULL THEN ''        ELSE delimit || UPPER(col7) END
          || CASE WHEN col8 IS NULL THEN ''        ELSE delimit || UPPER(col8) END
          || CASE WHEN col9 IS NULL THEN ''        ELSE delimit || UPPER(col9) END
          || CASE WHEN col10 IS NULL THEN ''       ELSE delimit || UPPER(col10) END;
   END create_pk_id;
END;
/

