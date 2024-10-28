use beautysalonglobaldb;
DELIMITER //

CREATE PROCEDURE CalculateLocalMetrics(
    IN local_db_id INT,
    IN report_start TIMESTAMP,
    IN report_end TIMESTAMP,
    OUT out_total_clients INT,
    OUT out_total_income DECIMAL(10, 2)
)
BEGIN
    DECLARE db_name VARCHAR(64);
    DECLARE total_clients INT DEFAULT 0;
    DECLARE total_income DECIMAL(10, 2) DEFAULT 0.00;

    SET db_name = CONCAT('beautysalonlocaldb', local_db_id);

    SET @query_clients = CONCAT('SELECT COUNT(DISTINCT customer_id) INTO @total_clients FROM ', db_name, '.appointments ',
        ' WHERE DATE(date) BETWEEN DATE(?) AND DATE(?) and status = "success"');
    
    PREPARE stmt_clients FROM @query_clients;
    SET @report_start = report_start;
    SET @report_end = report_end;
    EXECUTE stmt_clients USING @report_start, @report_end;
    
    SELECT @total_clients INTO out_total_clients;

    DEALLOCATE PREPARE stmt_clients;

    SET @query_income = CONCAT('SELECT IFNULL(SUM(s.price), 0) INTO @total_income FROM ', db_name, '.appointments a ',
        'JOIN ', db_name, '.services s ON a.service_id = s.id ',
        'WHERE DATE(a.date) BETWEEN DATE(?) AND DATE(?) and a.status = "success"');

    PREPARE stmt_income FROM @query_income;
    EXECUTE stmt_income USING @report_start, @report_end;

    SELECT @total_income INTO out_total_income;

    DEALLOCATE PREPARE stmt_income;

END//

DELIMITER ;
