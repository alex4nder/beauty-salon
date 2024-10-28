-- Global Database Schema: beautysalonglobaldb
create database beautysalonglobaldb;

USE beautysalonglobaldb;

-- Branches Table
CREATE TABLE branches (
    id INT PRIMARY KEY AUTO_INCREMENT,
    location VARCHAR(255),
    title VARCHAR(255),
    phone VARCHAR(18),
    contact_info VARCHAR(255)
);

-- Managers Table
CREATE TABLE managers (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    branch_id INT NOT NULL,
    first_name VARCHAR(36),
    last_name VARCHAR(36),
    phone VARCHAR(19),
    email VARCHAR(36),
    FOREIGN KEY (branch_id) REFERENCES branches(id) ON DELETE CASCADE
);

-- Global Reports Table
CREATE TABLE global_reports (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    branch_id INT NOT NULL,
    report_date DATE NOT NULL,
    start_date TIMESTAMP NOT NULL,
    end_date TIMESTAMP NOT NULL,
    clients_served INTEGER,
    total_income DECIMAL(10, 2),
    data TEXT,
    FOREIGN KEY (branch_id) REFERENCES branches(id) ON DELETE CASCADE
);

-- Customer Review Table
CREATE TABLE customer_review (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    customer_id CHAR(36) NOT NULL,
    branch_id INT NOT NULL,
    rate INTEGER CHECK (rate BETWEEN 1 AND 5),
    comment VARCHAR(255),
    created_at DATE,
    updated_at DATE,
    FOREIGN KEY (branch_id) REFERENCES branches(id) ON DELETE CASCADE
);

INSERT INTO branches (location, title, phone, contact_info) VALUES
('New York', 'Beauty Salon NYC', '123-456-7890', 'contact@nycbeautysalon.com'),
('Los Angeles', 'Beauty Salon LA', '987-654-3210', 'contact@labeautysalon.com'),
('Chicago', 'Beauty Salon Chicago', '555-555-5555', 'contact@chicagobeautysalon.com');

INSERT INTO managers (branch_id, first_name, last_name, phone, email) VALUES
((SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 'John', 'Doe', '111-222-3333', 'john.doe@nycbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 'Jane', 'Smith', '222-333-4444', 'jane.smith@nycbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 'Emily', 'Johnson', '333-444-5555', 'emily.johnson@labeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 'Michael', 'Brown', '444-555-6666', 'michael.brown@labeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 'Chris', 'Davis', '555-666-7777', 'chris.davis@chicagobeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 'Sarah', 'Miller', '666-777-8888', 'sarah.miller@chicagobeautysalon.com');

INSERT INTO global_reports (branch_id, report_date, start_date, end_date, clients_served, total_income, data) VALUES
((SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), '2024-10-01', '2024-10-01 09:00:00', '2024-10-01 18:00:00', 100, 5000.00, 'Income report for NYC'),
((SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), '2024-10-01', '2024-10-01 09:00:00', '2024-10-01 18:00:00', 80, 0.00, 'Customer report for NYC'),
((SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), '2024-10-01', '2024-10-01 09:00:00', '2024-10-01 18:00:00', 70, 0.00, 'Service report for NYC'),

((SELECT id FROM branches WHERE title = 'Beauty Salon LA'), '2024-10-01', '2024-10-01 10:00:00', '2024-10-01 19:00:00', 90, 4500.00, 'Income report for LA'),
((SELECT id FROM branches WHERE title = 'Beauty Salon LA'), '2024-10-01', '2024-10-01 10:00:00', '2024-10-01 19:00:00', 70, 0.00, 'Customer report for LA'),
((SELECT id FROM branches WHERE title = 'Beauty Salon LA'), '2024-10-01', '2024-10-01 10:00:00', '2024-10-01 19:00:00', 60, 0.00, 'Service report for LA'),

((SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'),  '2024-10-01', '2024-10-01 08:30:00', '2024-10-01 17:30:00', 110, 6000.00, 'Income report for Chicago'),
((SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'),  '2024-10-01', '2024-10-01 08:30:00', '2024-10-01 17:30:00', 95, 0.00, 'Customer report for Chicago'),
((SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'),  '2024-10-01', '2024-10-01 08:30:00', '2024-10-01 17:30:00', 85, 0.00, 'Service report for Chicago');


INSERT INTO customer_review (customer_id, branch_id, rate, comment, created_at, updated_at) VALUES
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 5, 'Great service!', '2024-10-05', '2024-10-06'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 4, 'Loved the atmosphere.', '2024-10-07', '2024-10-08'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 3, 'It was okay.', '2024-10-09', '2024-10-10'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 5, 'Amazing experience!', '2024-10-05', '2024-10-06'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 4, 'Very professional staff.', '2024-10-07', '2024-10-08'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 2, 'Not what I expected.', '2024-10-09', '2024-10-10'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 5, 'Best salon in the city!', '2024-10-05', '2024-10-06'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 4, 'I will come back for sure!', '2024-10-07', '2024-10-08'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 3, 'Decent, but could improve.', '2024-10-09', '2024-10-10'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 4, 'Had a good time!', '2024-10-11', '2024-10-12'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 5, 'Highly recommend!', '2024-10-11', '2024-10-12'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 2, 'Service was slow.', '2024-10-11', '2024-10-12'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon NYC'), 1, 'Very disappointed.', '2024-10-11', '2024-10-12'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon LA'), 3, 'Average experience.', '2024-10-11', '2024-10-12'),
(UUID(), (SELECT id FROM branches WHERE title = 'Beauty Salon Chicago'), 5, 'Exceptional service!', '2024-10-11', '2024-10-12');
