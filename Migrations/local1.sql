create database beautysalonlocaldb1;
create database beautysalonlocaldb2;
create database beautysalonlocaldb3;

USE beautysalonlocaldb1;

-- Customers Table
CREATE TABLE customers (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    phone VARCHAR(18),
    email VARCHAR(36) UNIQUE,
    birthday DATE
);

-- Services Table
CREATE TABLE services (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    title VARCHAR(36) NOT NULL,
    description VARCHAR(255),
    price DECIMAL(10, 2),
    duration INTEGER
);

-- Employees Table
CREATE TABLE employees (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    phone VARCHAR(18),
    email VARCHAR(36),
    position VARCHAR(100)
);

-- Appointments Table
CREATE TABLE appointments (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    service_id CHAR(36) NOT NULL,
    employee_id CHAR(36) NOT NULL,
    customer_id CHAR(36) NOT NULL,
    description VARCHAR(255),
    date DATE NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    status ENUM('created', 'success', 'cancelled') NOT NULL,
    FOREIGN KEY (service_id) REFERENCES services(id) ON DELETE CASCADE,
    FOREIGN KEY (employee_id) REFERENCES employees(id) ON DELETE CASCADE,
    FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
);

-- Schedule Table
CREATE TABLE schedule (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    employee_id CHAR(36) NOT NULL,
    date DATE NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES employees(id) ON DELETE CASCADE
);

INSERT INTO employees (first_name, last_name, email, phone, position) VALUES
('Alice', 'Smith', 'alice.smith@example.com', '555-123-4567', 'Stylist'),
('Bob', 'Johnson', 'bob.johnson@example.com', '555-234-5678', 'Colorist'),
('Cathy', 'Davis', 'cathy.davis@example.com', '555-345-6789', 'Nail Technician'),
('David', 'Brown', 'david.brown@example.com', '555-456-7890', 'Esthetician'),
('Ella', 'Wilson', 'ella.wilson@example.com', '555-567-8901', 'Stylist'),
('Frank', 'Martinez', 'frank.martinez@example.com', '555-678-9012', 'Nail Technician'),
('Grace', 'Garcia', 'grace.garcia@example.com', '555-789-0123', 'Manager'),
('Henry', 'Lopez', 'henry.lopez@example.com', '555-890-1234', 'Manager');


INSERT INTO customers (first_name, last_name, phone, email, birthday) VALUES
('Alice', 'Johnson', '123-456-7891', 'alice.johnson@example.com', '1990-01-01'),
('Bob', 'Smith', '123-456-7892', 'bob.smith@example.com', '1991-02-02'),
('Cathy', 'Brown', '123-456-7893', 'cathy.brown@example.com', '1992-03-03'),
('David', 'Wilson', '123-456-7894', 'david.wilson@example.com', '1993-04-04'),
('Ella', 'Davis', '123-456-7895', 'ella.davis@example.com', '1994-05-05'),
('Frank', 'Garcia', '123-456-7896', 'frank.garcia@example.com', '1995-06-06'),
('Grace', 'Martinez', '123-456-7897', 'grace.martinez@example.com', '1996-07-07'),
('Henry', 'Lopez', '123-456-7898', 'henry.lopez@example.com', '1997-08-08'),
('Ivy', 'Hernandez', '123-456-7899', 'ivy.hernandez@example.com', '1998-09-09'),
('Jack', 'Gonzalez', '123-456-7800', 'jack.gonzalez@example.com', '1999-10-10'),
('Lily', 'Wilson', '123-456-7801', 'lily.wilson@example.com', '2000-11-11'),
('Mia', 'Thompson', '123-456-7802', 'mia.thompson@example.com', '2001-12-12'),
('Nina', 'Garcia', '123-456-7803', 'nina.garcia@example.com', '1995-07-15'),
('Oliver', 'Martinez', '123-456-7804', 'oliver.martinez@example.com', '1998-08-18'),
('Paula', 'Lopez', '123-456-7805', 'paula.lopez@example.com', '2000-09-19');

INSERT INTO services (title, description, price, duration) VALUES
('Haircut', 'Standard haircut for men and women.', 20.00, 30),
('Hair Color', 'Full hair coloring service.', 75.00, 90),
('Hair Treatment', 'Deep conditioning and repair treatment.', 50.00, 45),
('Styling', 'Blowout and styling service.', 30.00, 60),
('Manicure', 'Nail trimming and polishing.', 15.00, 45),
('Pedicure', 'Foot care and nail polishing.', 25.00, 60),
('Facial', 'Deep cleansing facial treatment.', 50.00, 60),
('Massage', 'Relaxing full body massage.', 80.00, 60),
('Waxing', 'Hair removal service for legs and arms.', 35.00, 30),
('Eyebrow Shaping', 'Shaping and grooming of eyebrows.', 20.00, 15),
('Makeup Application', 'Professional makeup application.', 50.00, 30),
('Bridal Makeup', 'Specialized makeup for brides.', 100.00, 120),
('Keratin Treatment', 'Smoothing treatment for frizzy hair.', 150.00, 120);

INSERT INTO appointments (customer_id, service_id, employee_id, description, date, start_time, end_time, status) VALUES
((SELECT id FROM customers WHERE email = 'bob.smith@example.com'), (SELECT id FROM services WHERE title = 'Hair Color'), (SELECT id FROM employees WHERE email = 'grace.garcia@example.com'), 'Coloring appointment', '2024-10-16', '2024-10-16 11:00:00', '2024-10-16 12:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'cathy.brown@example.com'), (SELECT id FROM services WHERE title = 'Facial'), (SELECT id FROM employees WHERE email = 'david.brown@example.com'), 'Facial treatment', '2024-10-17', '2024-10-17 14:00:00', '2024-10-17 15:00:00', 'created'),
((SELECT id FROM customers WHERE email = 'david.wilson@example.com'), (SELECT id FROM services WHERE title = 'Massage'), (SELECT id FROM employees WHERE email = 'ella.wilson@example.com'), 'Relaxing massage', '2024-10-18', '2024-10-18 16:00:00', '2024-10-18 17:00:00', 'created'),
((SELECT id FROM customers WHERE email = 'ella.davis@example.com'), (SELECT id FROM services WHERE title = 'Pedicure'), (SELECT id FROM employees WHERE email = 'frank.martinez@example.com'), 'Nail care appointment', '2024-10-19', '2024-10-19 09:00:00', '2024-10-19 09:45:00', 'created'),
((SELECT id FROM customers WHERE email = 'frank.garcia@example.com'), (SELECT id FROM services WHERE title = 'Hair Treatment'), (SELECT id FROM employees WHERE email = 'bob.johnson@example.com'), 'Hair repair treatment', '2024-10-20', '2024-10-20 10:30:00', '2024-10-20 11:15:00', 'created'),
((SELECT id FROM customers WHERE email = 'grace.martinez@example.com'), (SELECT id FROM services WHERE title = 'Manicure'), (SELECT id FROM employees WHERE email = 'alice.smith@example.com'), 'Nail trimming and polishing', '2024-10-21', '2024-10-21 13:00:00', '2024-10-21 13:45:00', 'created'),
((SELECT id FROM customers WHERE email = 'henry.lopez@example.com'), (SELECT id FROM services WHERE title = 'Waxing'), (SELECT id FROM employees WHERE email = 'cathy.davis@example.com'), 'Leg waxing appointment', '2024-10-22', '2024-10-22 15:00:00', '2024-10-22 15:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'ivy.hernandez@example.com'), (SELECT id FROM services WHERE title = 'Styling'), (SELECT id FROM employees WHERE email = 'david.brown@example.com'), 'Styling appointment', '2024-10-23', '2024-10-23 10:00:00', '2024-10-23 11:00:00', 'created'),
((SELECT id FROM customers WHERE email = 'jack.gonzalez@example.com'), (SELECT id FROM services WHERE title = 'Makeup Application'), (SELECT id FROM employees WHERE email = 'grace.garcia@example.com'), 'Makeup for event', '2024-10-24', '2024-10-24 12:00:00', '2024-10-24 12:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'lily.wilson@example.com'), (SELECT id FROM services WHERE title = 'Bridal Makeup'), (SELECT id FROM employees WHERE email = 'frank.martinez@example.com'), 'Bridal makeup consultation', '2024-10-25', '2024-10-25 09:30:00', '2024-10-25 10:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'mia.thompson@example.com'), (SELECT id FROM services WHERE title = 'Keratin Treatment'), (SELECT id FROM employees WHERE email = 'alice.smith@example.com'), 'Smoothing treatment for hair', '2024-10-26', '2024-10-26 14:00:00', '2024-10-26 16:00:00', 'created');

INSERT INTO schedule (employee_id, date, start_time, end_time) VALUES
((SELECT id FROM employees WHERE email = 'alice.smith@example.com'), '2024-10-18', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alice.smith@example.com'), '2024-10-19', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alice.smith@example.com'), '2024-10-20', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alice.smith@example.com'), '2024-10-21', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alice.smith@example.com'), '2024-10-22', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'bob.johnson@example.com'), '2024-10-12', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'bob.johnson@example.com'), '2024-10-19', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'bob.johnson@example.com'), '2024-10-11', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'cathy.davis@example.com'), '2024-10-20', '08:30:00', '16:30:00'),
((SELECT id FROM employees WHERE email = 'cathy.davis@example.com'), '2024-10-22', '08:30:00', '16:30:00'),
((SELECT id FROM employees WHERE email = 'david.brown@example.com'), '2024-10-23', '09:00:00', '15:00:00'),
((SELECT id FROM employees WHERE email = 'ella.wilson@example.com'), '2024-10-23', '10:00:00', '16:00:00');

