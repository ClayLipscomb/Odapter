CREATE OR REPLACE PACKAGE ODPT.odpt_pkg_sql IS
   
   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_value IN VARCHAR2 );
--   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_value IN DATE );
   PROCEDURE where_condition_param (p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_operator IN VARCHAR2, p_param_name IN VARCHAR2, p_param_value IN TIMESTAMP);
   
   PROCEDURE where_condition(p_where IN OUT VARCHAR2, p_operand1 IN VARCHAR2, p_operator IN VARCHAR2, p_operand2 IN VARCHAR2, p_or IN BOOLEAN );           
   PROCEDURE where_condition(p_where IN OUT VARCHAR2, p_operand1 IN VARCHAR2, p_operator IN VARCHAR2, p_operand2 IN VARCHAR2 );           
   
   PROCEDURE where_condition_range_param(p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_from_param_name IN VARCHAR2, p_from_param_value IN VARCHAR2,
                                          p_to_param_name IN VARCHAR2, p_to_param_value IN VARCHAR2 );           
--   PROCEDURE where_condition_range_param(p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_from_param_name IN VARCHAR2, p_from_param_value IN DATE,
--                                          p_to_param_name IN VARCHAR2, p_to_param_value IN DATE );           
   PROCEDURE where_condition_range_param(p_where IN OUT VARCHAR2, p_column IN VARCHAR2, p_from_param_name IN VARCHAR2, p_from_param_value IN TIMESTAMP,
                                          p_to_param_name IN VARCHAR2, p_to_param_value IN TIMESTAMP );           
   
END odpt_pkg_sql;
/