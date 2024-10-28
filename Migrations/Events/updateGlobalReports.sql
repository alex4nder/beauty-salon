DELIMITER //

CREATE EVENT `UpdateGlobalReportsEvent`
ON SCHEDULE EVERY 7 DAY 
STARTS CURRENT_DATE + INTERVAL 1 DAY 
DO
BEGIN
    DECLARE local_db_id INT DEFAULT 1; 
    DECLARE report_start TIMESTAMP DEFAULT DATE_SUB(NOW(), INTERVAL 7 DAY);
    DECLARE report_end TIMESTAMP DEFAULT NOW();


    CALL UpdateGlobalReports(local_db_id, report_start, report_end);
END //

DELIMITER ;
