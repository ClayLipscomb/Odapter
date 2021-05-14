--DROP TABLE ODPT.odpt_table_ignore_any CASCADE CONSTRAINTS;

CREATE TABLE ODPT.odpt_table_ignore_any (
	col_anydata                     ANYDATA,
    col_anydataset                  ANYDATASET,
    col_anytype                     ANYTYPE
);