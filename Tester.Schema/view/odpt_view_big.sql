--DROP VIEW ODPT.odpt_view_big_v;
       
CREATE OR REPLACE FORCE VIEW ODPT.odpt_view_big_v AS
SELECT *
FROM ODPT.odpt_table_big;