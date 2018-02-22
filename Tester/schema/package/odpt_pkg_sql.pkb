CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_sql AS

  /******************************************************************************
   NAME:       where_condition_param
   PURPOSE: AND/OR a param condition to a where clause handling the case when the param is NULL
   REVISIONS:
   Date        Author           Description
   ----------  ---------------  ----------------------------------------------
    11/05/15 Lipscomb        created
******************************************************************************/
   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_has_value IN BOOLEAN, p_or IN BOOLEAN ) IS
   BEGIN                
    p_where := p_where || CHR(10) || (CASE WHEN p_or THEN ' OR ' ELSE ' AND ' END);            
    IF p_param_has_value THEN
        p_where := p_where || ' ' || TRIM(p_column) || ' ' || TRIM(p_operator) || ' :' || UPPER(TRIM(p_param_name)) || ' ';            
    ELSE
        p_where := p_where || ' (1=1 OR :' || UPPER(TRIM(p_param_name)) || ' IS NULL) ';            
    END IF;             
   END where_condition_param;

   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_has_value IN BOOLEAN ) IS
   BEGIN                
    where_condition_param(p_where, p_column, p_operator, p_param_name, p_param_has_value, FALSE);
   END where_condition_param;

   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_value IN VARCHAR2 ) IS
   BEGIN           
      where_condition_param(p_where, p_column, p_operator, p_param_name, (p_param_value IS NOT NULL));     
   END where_condition_param;

   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_value IN DATE ) IS
   BEGIN                
      where_condition_param(p_where, p_column, p_operator, p_param_name, (p_param_value IS NOT NULL));     
   END where_condition_param;

   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_value IN TIMESTAMP ) IS
   BEGIN                
      where_condition_param(p_where, p_column, p_operator, p_param_name, (p_param_value IS NOT NULL));     
   END where_condition_param;
  
  /******************************************************************************
   NAME:       where_condition
   PURPOSE:  AND/OR a generic condition to a where clause
   REVISIONS:
   Date        Author           Description
   ----------  ---------------  ----------------------------------------------
    11/05/15 Lipscomb        created
******************************************************************************/
   PROCEDURE where_condition(p_where IN OUT VARCHAR2, p_operand1 IN VARCHAR2, p_operator IN VARCHAR2, p_operand2 IN VARCHAR2, p_or IN BOOLEAN ) IS
   BEGIN
      p_where := p_where || CHR(10) || (CASE WHEN p_or THEN ' OR ' ELSE ' AND ' END) || TRIM(p_operand1) || ' ' || TRIM(p_operator) || ' ' || TRIM(p_operand2) || ' ';            
   END where_condition;

   PROCEDURE where_condition(p_where IN OUT VARCHAR2, p_operand1 IN VARCHAR2, p_operator IN VARCHAR2, p_operand2 IN VARCHAR2 ) IS
   BEGIN
      where_condition(p_where, p_operand1, p_operator, p_operand2, FALSE);            
   END where_condition;

  /******************************************************************************
   NAME:       where_condition_range_param
   PURPOSE:  AND/OR a param from/to range condition to a where clause handling the case either param is NULL
   REVISIONS:
   Date        Author           Description
   ----------  ---------------  ----------------------------------------------
    11/05/15 Lipscomb        created
******************************************************************************/
   PROCEDURE where_condition_range_param(p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_from_param_name IN VARCHAR2, p_from_param_value IN VARCHAR2,
                                          p_to_param_name IN VARCHAR2, p_to_param_value IN VARCHAR2 ) IS
    BEGIN
        where_condition_param(p_where, p_column, '>=', p_from_param_name, p_from_param_value);
        where_condition_param(p_where, p_column, '<=', p_to_param_name, p_to_param_value);
    END where_condition_range_param;           

   PROCEDURE where_condition_range_param(p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_from_param_name IN VARCHAR2, p_from_param_value IN DATE,
                                          p_to_param_name IN VARCHAR2, p_to_param_value IN DATE ) IS
    BEGIN
        where_condition_param(p_where, p_column, '>=', p_from_param_name, p_from_param_value);
        where_condition_param(p_where, p_column, '<=', p_to_param_name, p_to_param_value);
    END where_condition_range_param;           

   PROCEDURE where_condition_range_param(p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_from_param_name IN VARCHAR2, p_from_param_value IN TIMESTAMP,
                                          p_to_param_name IN VARCHAR2, p_to_param_value IN TIMESTAMP ) IS
    BEGIN
        where_condition_param(p_where, p_column, '>=', p_from_param_name, p_from_param_value);
        where_condition_param(p_where, p_column, '<=', p_to_param_name, p_to_param_value);
    END where_condition_range_param;           

END odpt_pkg_sql;
/
