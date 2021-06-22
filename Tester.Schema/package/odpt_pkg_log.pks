CREATE OR REPLACE PACKAGE ODPT.odpt_pkg_log AS

  -- The implemented debug methods 
  G_METHOD_DBMS_OUT CONSTANT NUMBER := 1;
  G_METHOD_CACHE CONSTANT NUMBER := 2;

  G_DBMS_OUTPUT_BUFFER_SIZE NUMBER := 200000;
  
  -- database logging severity types
  SEVERITY_DEBUG CONSTANT SMALLINT := 0; 
  SEVERITY_INFO CONSTANT SMALLINT := 1; 
  SEVERITY_WARNING CONSTANT SMALLINT := 2; 
  SEVERITY_ERROR CONSTANT SMALLINT := 3; 
  
  -- our weakly typed cursor type
  TYPE t_ref_cursor IS REF CURSOR;
  
  -- associative array types
  TYPE t_assocarray_user_name IS TABLE OF odpt_logs.user_name%TYPE INDEX BY PLS_INTEGER;  
  TYPE t_assocarray_source IS TABLE OF odpt_logs.source%TYPE INDEX BY PLS_INTEGER;  
  TYPE t_assocarray_interface IS TABLE OF odpt_logs.interface%TYPE INDEX BY PLS_INTEGER;  
  TYPE t_assocarray_severity IS TABLE OF odpt_logs.severity%TYPE INDEX BY PLS_INTEGER;  
  TYPE t_assocarray_severity_desc IS TABLE OF VARCHAR2(10) INDEX BY PLS_INTEGER;  
  
  -- Procedures 
  procedure log_msg(p_pkg varchar2, p_proc varchar2, p_msg varchar2);
  procedure set_log_on(p_method number default G_METHOD_DBMS_OUT);
  procedure set_log_off;
  PROCEDURE insert_log (p_severity IN odpt_logs.severity%TYPE, p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, 
                        p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL);        

  PROCEDURE insert_log_debug (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL);
  PROCEDURE insert_log_info (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL);
  PROCEDURE insert_log_warning (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL);
  PROCEDURE insert_log_error (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL);
                                                
  PROCEDURE delete_log(p_severity IN odpt_logs.severity%TYPE, p_to_date odpt_logs.log_dt%TYPE); 
  
  FUNCTION get_log (p_log_dt_from odpt_logs.log_dt%TYPE DEFAULT NULL, 
                    p_log_dt_to odpt_logs.log_dt%TYPE DEFAULT NULL,
                    p_user_name odpt_logs.user_name%TYPE DEFAULT NULL, 
                    p_interface odpt_logs.interface%TYPE DEFAULT NULL, 
                    p_severity odpt_logs.severity%TYPE DEFAULT NULL, 
                    p_source odpt_logs.source%TYPE DEFAULT NULL ) RETURN t_ref_cursor;
                     
  PROCEDURE get_distinct_criteria (p_sources OUT t_assocarray_source, p_interfaces OUT t_assocarray_interface, p_user_names OUT t_assocarray_user_name, 
      p_max_age_in_days NUMBER DEFAULT NULL);
  
  PROCEDURE get_severity (p_severities OUT t_assocarray_severity, p_severity_descriptions OUT t_assocarray_severity_desc);
  
  FUNCTION type_severity_debug RETURN odpt_logs.severity%TYPE;
  FUNCTION type_severity_info RETURN odpt_logs.severity%TYPE;
  FUNCTION type_severity_warning RETURN odpt_logs.severity%TYPE;
  FUNCTION type_severity_error RETURN odpt_logs.severity%TYPE;

END odpt_pkg_log;
/