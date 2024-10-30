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
('Москва', 'Салон красоты Москва', '123-456-7890', 'contact@moscowbeautysalon.com'),
('Санкт-Петербург', 'Салон красоты СПб', '987-654-3210', 'contact@spbbeautysalon.com'),
('Новосибирск', 'Салон красоты Новосибирск', '555-555-5555', 'contact@nskbeautysalon.com');

INSERT INTO managers (branch_id, first_name, last_name, phone, email) VALUES
((SELECT id FROM branches WHERE title = 'Салон красоты Москва'), 'Иван', 'Иванов', '111-222-3333', 'ivan.ivanov@moscowbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Салон красоты Москва'), 'Мария', 'Петрова', '222-333-4444', 'maria.petrova@moscowbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Салон красоты СПб'), 'Ольга', 'Сидорова', '333-444-5555', 'olga.sidorova@spbbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Салон красоты СПб'), 'Дмитрий', 'Смирнов', '444-555-6666', 'dmitry.smirnov@spbbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Салон красоты Новосибирск'), 'Алексей', 'Кузнецов', '555-666-7777', 'alexey.kuznetsov@nskbeautysalon.com'),
((SELECT id FROM branches WHERE title = 'Салон красоты Новосибирск'), 'Елена', 'Волкова', '666-777-8888', 'elena.volkova@nskbeautysalon.com');

INSERT INTO global_reports (branch_id, report_date, start_date, end_date, clients_served, total_income, data) VALUES
((SELECT id FROM branches WHERE title = 'Салон красоты Москва'), '2024-10-01', '2024-10-01 09:00:00', '2024-10-01 18:00:00', 100, 5000.00, 'Отчет о доходах для Москвы'),
((SELECT id FROM branches WHERE title = 'Салон красоты Москва'), '2024-10-01', '2024-10-01 09:00:00', '2024-10-01 18:00:00', 80, 0.00, 'Отчет по клиентам для Москвы'),
((SELECT id FROM branches WHERE title = 'Салон красоты Москва'), '2024-10-01', '2024-10-01 09:00:00', '2024-10-01 18:00:00', 70, 0.00, 'Отчет по услугам для Москвы'),

((SELECT id FROM branches WHERE title = 'Салон красоты СПб'), '2024-10-01', '2024-10-01 10:00:00', '2024-10-01 19:00:00', 90, 4500.00, 'Отчет о доходах для СПб'),
((SELECT id FROM branches WHERE title = 'Салон красоты СПб'), '2024-10-01', '2024-10-01 10:00:00', '2024-10-01 19:00:00', 70, 0.00, 'Отчет по клиентам для СПб'),
((SELECT id FROM branches WHERE title = 'Салон красоты СПб'), '2024-10-01', '2024-10-01 10:00:00', '2024-10-01 19:00:00', 60, 0.00, 'Отчет по услугам для СПб'),

((SELECT id FROM branches WHERE title = 'Салон красоты Новосибирск'), '2024-10-01', '2024-10-01 08:30:00', '2024-10-01 17:30:00', 110, 6000.00, 'Отчет о доходах для Новосибирска'),
((SELECT id FROM branches WHERE title = 'Салон красоты Новосибирск'), '2024-10-01', '2024-10-01 08:30:00', '2024-10-01 17:30:00', 95, 0.00, 'Отчет по клиентам для Новосибирска'),
((SELECT id FROM branches WHERE title = 'Салон красоты Новосибирск'), '2024-10-01', '2024-10-01 08:30:00', '2024-10-01 17:30:00', 85, 0.00, 'Отчет по услугам для Новосибирска');

INSERT INTO customer_review (customer_id, branch_id, rate, comment, created_at, updated_at) VALUES
('dc8e77de-96f5-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Москва'), 5, 'Отличное обслуживание!', '2024-10-05', '2024-10-06'),
('dc8e7e0b-96f5-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Москва'), 5, 'Очень понравилась атмосфера.', '2024-10-07', '2024-10-08'),
('c5e748f0-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты СПб'), 5, 'Прекрасный опыт!', '2024-10-05', '2024-10-06'),
('c5e74b73-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты СПб'), 4, 'Очень профессиональный персонал.', '2024-10-07', '2024-10-08'),
('c5e74d6d-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты СПб'), 2, 'Не то, что ожидал.', '2024-10-09', '2024-10-10'),
('c8a0576b-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Новосибирск'), 5, 'Лучший салон в городе!', '2024-10-05', '2024-10-06'),
('c8a05b8f-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Новосибирск'), 5, 'Обязательно вернусь!', '2024-10-07', '2024-10-08'),
('c8a05c3a-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Новосибирск'), 4, 'Неплохо, но можно улучшить.', '2024-10-09', '2024-10-10'),
('dc8e7ea3-96f5-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Москва'), 4, 'Хорошо провел время!', '2024-10-11', '2024-10-12'),
('c5e74d0d-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты СПб'), 5, 'Рекомендую!', '2024-10-11', '2024-10-12'),
('c8a05a59-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Новосибирск'), 3, 'Обслуживание было медленным.', '2024-10-11', '2024-10-12'),
('dc8e8062-96f5-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Москва'), 1, 'Очень разочарован.', '2024-10-11', '2024-10-12'),
('c5e74dcb-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты СПб'), 3, 'Средний опыт.', '2024-10-11', '2024-10-12'),
('c8a05c3a-96f6-11ef-ba04-00ffa193333b', (SELECT id FROM beautysalonglobaldb.branches WHERE title = 'Салон красоты Новосибирск'), 1, 'Исключительное обслуживание!', '2024-10-11', '2024-10-12');
