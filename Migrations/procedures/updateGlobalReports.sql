DELIMITER //

CREATE PROCEDURE `beautysalonglobaldb`.`UpdateGlobalReports`(
    IN local_db_id INT,
    IN report_start TIMESTAMP,
    IN report_end TIMESTAMP
)
BEGIN
    DECLARE total_clients INT;
    DECLARE total_income DECIMAL(10, 2);
    DECLARE report_count INT;

    main_block: BEGIN

         SELECT COUNT(*) INTO report_count 
    FROM global_reports 
    WHERE branch_id = local_db_id
      AND start_date BETWEEN DATE_SUB(report_start, INTERVAL 6 HOUR) AND DATE_ADD(report_start, INTERVAL 6 HOUR)
      AND end_date BETWEEN DATE_SUB(report_start, INTERVAL 6 HOUR) AND DATE_ADD(report_end, INTERVAL 6 HOUR);
     
        IF report_count > 0 THEN
            LEAVE main_block; 
        END IF;

        CALL CalculateLocalMetrics(local_db_id, report_start, report_end, total_clients, total_income);

        INSERT INTO global_reports (branch_id, report_date, start_date, end_date, clients_served, total_income, data)
        VALUES (
            local_db_id,
            CURRENT_TIMESTAMP,
            report_start,
            report_end,
            total_clients,
            total_income,
            CONCAT('Report for branch ID: ', local_db_id)
        )
        ON DUPLICATE KEY UPDATE
            clients_served = total_clients,
            total_income = total_income,
            data = CONCAT('Updated report for branch ID: ', local_db_id);

    END main_block; 

END//

DELIMITER ;