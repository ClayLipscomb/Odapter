CREATE OR REPLACE PACKAGE BODY ODPT.odpt_pkg_log AS
   -- Globals 
  -- Is DBMS_OUTPUT logging turned on 
  G_LOGGING BOOLEAN := FALSE;
  
  -- Default the log method to dbms_out 
  G_LOG_METHOD NUMBER := G_METHOD_DBMS_OUT;
  G_PACKAGE CONSTANT VARCHAR2(30) := 'PACKAGE_LOG';
  
  -- enables logging to database
  DEBUG_MODE CONSTANT CHAR(1) := 'N';

  procedure set_log_on(p_method number default G_METHOD_DBMS_OUT) is
    l_procedure varchar2(30) := 'SET_LOG_ON';
  begin
    -- Turn logging on
    g_logging := true;
    -- Set the method
    if(p_method = G_METHOD_DBMS_OUT) then
     G_LOG_METHOD := G_METHOD_DBMS_OUT; 
     DBMS_OUTPUT.ENABLE(G_DBMS_OUTPUT_BUFFER_SIZE);
    elsif(p_method = G_METHOD_CACHE) then
     null; -- initialize the cache
    end if;
  end set_log_on;

  procedure set_log_off is
    l_procedure varchar2(30) := 'SET_LOG_OFF';
  begin
    -- Turn logging off
    g_logging := false;
    -- Set the method
    if(G_LOG_METHOD = G_METHOD_DBMS_OUT) then
     DBMS_OUTPUT.DISABLE;
    elsif(G_LOG_METHOD  = G_METHOD_CACHE) then
     null; -- clear cache
    end if;
  end set_log_off;

  procedure log_msg(p_pkg varchar2, p_proc varchar2, p_msg varchar2) is
    l_procedure varchar2(30) := 'LOG_MSG';
  begin
    if(g_logging) then
      if (g_log_method = G_METHOD_DBMS_OUT) then
        dbms_output.put_line(p_pkg||'.'||p_proc||' - '||p_msg);
      end if;
    end if;
  end log_msg;

  --*******************************************************************************
  -- DATABASE LOGGING
  --*******************************************************************************
  
  PROCEDURE insert_log (p_severity IN odpt_logs.severity%TYPE, p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, 
                        p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL) IS        
                                     
      pragma autonomous_transaction;                                     
      logID       NUMBER;
      msg         VARCHAR2(4000);
      source      VARCHAR2(100);
      user_name   VARCHAR2(128);           
      interface   VARCHAR2(200);           
   BEGIN
      -- get primary key   
      SELECT odpt_logs_s.NEXTVAL INTO logID FROM dual;
      
      -- handle user name
      IF (p_user_name IS NULL) THEN
         user_name := '**ANONYMOUS**';
      ELSIF (LENGTH(p_user_name) > 128) THEN
         user_name := UPPER(SUBSTR(p_user_name, 1, 128));   
      ELSE
         user_name := UPPER(p_user_name);   
      END IF;
      
      -- handle msg
      IF (p_msg IS NULL) THEN
         msg := 'N/A';
      ELSIF (LENGTH(p_msg) > 4000) THEN 
         msg := SUBSTR(p_msg, 1, 4000);
      ELSE
         msg := p_msg;
      END IF;

      -- handle source
      IF (p_source IS NULL) THEN
         source := 'N/A';
      ELSIF (LENGTH(p_source) > 100) THEN 
         source := SUBSTR(p_source, 1, 100);
      ELSE
         source := p_source;
      END IF;

      IF (p_interface IS NULL) THEN
         interface := 'N/A';
      ELSIF (LENGTH(p_interface) > 200) THEN 
         interface := SUBSTR(p_interface, 1, 200);
      ELSE
         interface := p_interface;
      END IF;
   
      INSERT INTO odpt_logs (log_id, log_dt, user_name, interface, source, msg, severity)
      VALUES (logID, SYSTIMESTAMP, user_name, interface, source, msg, p_severity);
      COMMIT;
      RETURN;
   END;

   PROCEDURE insert_log_debug (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, 
         p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL) IS
   BEGIN
      insert_log(type_severity_debug, p_source, p_msg, p_user_name, p_interface);
      RETURN;
   END;

   PROCEDURE insert_log_info (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL) IS
   BEGIN
      insert_log(type_severity_info, p_source, p_msg, p_user_name, p_interface);
      RETURN;
   END;

   PROCEDURE insert_log_warning (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL) IS
   BEGIN
      insert_log(type_severity_warning, p_source, p_msg, p_user_name, p_interface);
      RETURN;
   END;

   PROCEDURE insert_log_error (p_source IN odpt_logs.SOURCE%TYPE, p_msg IN odpt_logs.msg%TYPE, p_user_name IN odpt_logs.user_name%TYPE DEFAULT NULL, p_interface IN odpt_logs.interface%TYPE DEFAULT NULL) IS
   BEGIN
      insert_log(type_severity_error, p_source, p_msg, p_user_name, p_interface);
      RETURN;
   END;   

  PROCEDURE delete_log(p_severity IN odpt_logs.severity%TYPE, p_to_date odpt_logs.log_dt%TYPE) IS 
   BEGIN
      DELETE 
      FROM odpt_logs
      WHERE 1 = 1
        AND (p_severity IS NULL OR p_severity = severity)
        AND (p_to_date IS NULL OR log_dt <= p_to_date);
   END;
   
   FUNCTION type_severity_debug RETURN odpt_logs.severity%TYPE IS
   BEGIN
      RETURN SEVERITY_DEBUG;
   END;
   FUNCTION type_severity_info RETURN odpt_logs.severity%TYPE IS
   BEGIN
      RETURN SEVERITY_INFO;
   END;
   FUNCTION type_severity_warning RETURN odpt_logs.severity%TYPE IS
   BEGIN
      RETURN SEVERITY_WARNING;
   END;
   FUNCTION type_severity_error RETURN odpt_logs.severity%TYPE IS
   BEGIN
      RETURN SEVERITY_ERROR;
   END;

   FUNCTION get_log (p_log_dt_from odpt_logs.log_dt%TYPE DEFAULT NULL, 
                     p_log_dt_to odpt_logs.log_dt%TYPE DEFAULT NULL,
                     p_user_name odpt_logs.user_name%TYPE DEFAULT NULL, 
                     p_interface odpt_logs.interface%TYPE DEFAULT NULL, 
                     p_severity odpt_logs.severity%TYPE DEFAULT NULL, 
                     p_source odpt_logs.source%TYPE DEFAULT NULL ) RETURN t_ref_cursor IS
      l_dyncur t_ref_cursor;    
      l_select_from     VARCHAR2(1000) DEFAULT ' ';
      l_where           VARCHAR2(8000) DEFAULT ' ';
      l_order_by        VARCHAR2(1000) DEFAULT ' ';
      l_query           VARCHAR2(10000) DEFAULT ' ';      
   BEGIN    
      l_select_from := ' SELECT log_id, log_dt, user_name, severity, interface, source, msg FROM odpt.odpt_logs ';
      
      l_where := ' WHERE 1 = 1 ';
      odpt_pkg_sql.where_condition_range_param(l_where, 'log_dt',  'p_log_dt_from', p_log_dt_from, 'p_log_dt_to', p_log_dt_to);      
      odpt_pkg_sql.where_condition_param(l_where, 'user_name', '=', 'p_user_name', p_user_name);
      odpt_pkg_sql.where_condition_param(l_where, 'interface', '=', 'p_interface', p_interface);
      odpt_pkg_sql.where_condition_param(l_where, 'severity', '=', 'p_severity', p_severity);
      odpt_pkg_sql.where_condition_param(l_where, 'source', '=', 'p_source', p_source);
       
      l_order_by :=  ' ORDER BY log_dt DESC '; 
   
      l_query := l_select_from || ' ' || l_where || ' ' || l_order_by;
      
      OPEN l_dyncur FOR l_query USING p_log_dt_from, p_log_dt_to, p_user_name, p_interface, p_severity, p_source;
      RETURN l_dyncur;
    
  END get_log;

   PROCEDURE get_distinct_criteria (p_sources OUT t_assocarray_source, p_interfaces OUT t_assocarray_interface, p_user_names OUT t_assocarray_user_name, 
      p_max_age_in_days NUMBER DEFAULT NULL) IS
   BEGIN
      -- get unique sources
      SELECT DISTINCT source
      BULK COLLECT INTO p_sources 
      FROM odpt_logs
      WHERE log_dt > SYSDATE - NVL(p_max_age_in_days, 10000)
      ORDER BY source;

      -- get unique interfaces
      SELECT DISTINCT interface
      BULK COLLECT INTO p_interfaces 
      FROM odpt_logs
      WHERE log_dt > SYSDATE - NVL(p_max_age_in_days, 10000)
      ORDER BY interface;

      -- get unique interfaces
      SELECT DISTINCT user_name
      BULK COLLECT INTO p_user_names 
      FROM odpt_logs
      WHERE log_dt > SYSDATE - NVL(p_max_age_in_days, 10000)
      ORDER BY user_name;

   END get_distinct_criteria;

   PROCEDURE get_severity (p_severities OUT t_assocarray_severity, p_severity_descriptions OUT t_assocarray_severity_desc) IS
   BEGIN
      p_severities(1) := type_severity_debug;
      p_severities(2) := type_severity_info;
      p_severities(3) := type_severity_warning;
      p_severities(4) := type_severity_error;
      p_severity_descriptions(1) := 'DEBUG';
      p_severity_descriptions(2) := 'INFO';
      p_severity_descriptions(3) := 'WARNING';
      p_severity_descriptions(4) := 'ERROR';
      RETURN;
      
   END get_severity;
  
END odpt_pkg_log;
/