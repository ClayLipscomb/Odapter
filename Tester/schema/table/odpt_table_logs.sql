--DROP TABLE ODPT.odpt_logs CASCADE CONSTRAINTS;

CREATE TABLE ODPT.odpt_logs (
  log_id       INTEGER                       NOT NULL,
  log_dt       TIMESTAMP(6)                  NOT NULL,
  user_name    VARCHAR2(128)                 NOT NULL,
  severity     NUMBER(1)                     NOT NULL,
  interface    VARCHAR2(200)                 NULL,
  source       VARCHAR2(100)                 NOT NULL,
  msg          VARCHAR2(4000)                NOT NULL
);

CREATE UNIQUE INDEX ODPT.odpt_logs_pk ON ODPT.odpt_logs (log_id);
ALTER TABLE ODPT.odpt_logs ADD ( CONSTRAINT odpt_logs_pk PRIMARY KEY (log_id) USING INDEX ODPT.odpt_logs_pk);

