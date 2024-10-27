USE beautysalonlocaldb1;

CREATE TABLE clients (
    id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(100) UNIQUE,
    date_of_birth DATE,
    notes TEXT
);

CREATE TABLE employees (
    id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    position VARCHAR(50) NOT NULL,
    work_book_number VARCHAR(50) NOT NULL
);

CREATE TABLE work_hours (
    id INT PRIMARY KEY AUTO_INCREMENT,
    employee_id INTEGER NOT NULL,
    work_day VARCHAR(50) NOT NULL,
    start_time VARCHAR(50) NOT NULL,
    end_time VARCHAR(50) NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES employees(id)
);

CREATE TABLE Services (
    id INT PRIMARY KEY AUTO_INCREMENT,
    service_name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2),
    duration INTEGER
);

CREATE TABLE Appointments (
    id INT PRIMARY KEY AUTO_INCREMENT,
    client_id INTEGER NOT NULL,
    employee_id INTEGER NOT NULL,
    service_id INTEGER NOT NULL,
    appointment_date TIMESTAMP NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    status VARCHAR(20),
    FOREIGN KEY (client_id) REFERENCES clients(id),
    FOREIGN KEY (employee_id) REFERENCES employees(id),
    FOREIGN KEY (service_id) REFERENCES services(id)
);

ALTER TABLE appointments
DROP FOREIGN KEY appointments_ibfk_1;

ALTER TABLE appointments
DROP FOREIGN KEY appointments_ibfk_2;

ALTER TABLE appointments
DROP FOREIGN KEY appointments_ibfk_3;

ALTER TABLE appointments
ADD CONSTRAINT appointments_ibfk_1 FOREIGN KEY (client_id) 
REFERENCES clients(id) 
ON DELETE CASCADE;

ALTER TABLE appointments
ADD CONSTRAINT fk_appointments_employee_id FOREIGN KEY (employee_id) 
REFERENCES employees(id) 
ON DELETE CASCADE;

ALTER TABLE appointments
ADD CONSTRAINT fk_appointments_service_id FOREIGN KEY (service_id) 
REFERENCES services(id) 
ON DELETE CASCADE;

ALTER TABLE work_hours
DROP FOREIGN KEY work_hours_ibfk_1;

ALTER TABLE work_hours
ADD CONSTRAINT work_hours_ibfk_1 FOREIGN KEY (employee_id) 
REFERENCES employees(id) 
ON DELETE CASCADE;

CREATE INDEX idx_clients_last_name ON clients(last_name);
CREATE INDEX idx_employees_last_name ON employees(last_name);
CREATE INDEX idx_services_service_name ON services(service_name);
CREATE INDEX idx_appointments_date ON appointments(appointment_date);

INSERT INTO clients (first_name, last_name, phone, email, date_of_birth, notes) VALUES
('Иван', 'Иванов', '89991234567', 'ivan.ivanov@example.com', '1985-01-15', 'Постоянный клиент'),
('Мария', 'Петрова', '89992345678', 'maria.petrova@example.com', '1990-05-23', 'Предпочтительный клиент'),
('Ольга', 'Сидорова', '89993456789', 'olga.sidorova@example.com', '1992-11-11', NULL),
('Алексей', 'Смирнов', '89994567890', 'alexey.smirnov@example.com', '1988-07-07', 'Участвует в акциях'),
('Екатерина', 'Федорова', '89995678901', 'ekaterina.fedorova@example.com', '1983-03-03', 'Постоянные скидки'),
('Анна', 'Кузнецова', '89996789012', 'anna.kuznetsova@example.com', '1995-06-12', NULL),
('Дмитрий', 'Соколова', '89997890123', 'dmitry.sokolov@example.com', '1987-09-30', 'Требует особого ухода'),
('Елена', 'Попова', '89998901234', 'elena.popova@example.com', '1993-02-28', NULL),
('Александр', 'Новиков', '89999012345', 'alexander.novikov@example.com', '1991-08-15', 'Пришел по рекомендации'),
('Наталья', 'Морозова', '89990123456', 'natalya.morozova@example.com', '1989-12-19', 'Важный клиент'),
('Сергей', 'Волков', '89991234567', 'sergey.volkov@example.com', '1994-05-05', NULL),
('Татьяна', 'Зайцева', '89992345678', 'tatiana.zaitseva@example.com', '1996-10-10', NULL),
('Игорь', 'Борисов', '89993456789', 'igor.borisov@example.com', '1997-04-17', 'Иногда приносит подарки'),
('Оксана', 'Яковлева', '89994567890', 'oksana.yakovleva@example.com', '1986-01-29', NULL),
('Михаил', 'Григорьев', '89995678901', 'mikhail.grigoryev@example.com', '1992-07-04', 'Нуждается в дополнительном времени');

INSERT INTO employees (first_name, last_name, phone, position, work_book_number) VALUES
('Алексей', 'Иванов', '89991234567', 'Парикмахер', 'WB001'),
('Марина', 'Сидорова', '89992345678', 'Косметолог', 'WB002'),
('Ольга', 'Петрова', '89993456789', 'Массажист', 'WB003'),
('Иван', 'Федоров', '89994567890', 'Маникюрист', 'WB004'),
('Анна', 'Новикова', '89995678901', 'Визажист', 'WB005'),
('Дмитрий', 'Кузнецов', '89996789012', 'Парикмахер', 'WB006'),
('Екатерина', 'Морозова', '89997890123', 'Косметолог', 'WB007'),
('Сергей', 'Борисов', '89998901234', 'Массажист', 'WB008'),
('Александр', 'Попов', '89999012345', 'Мастер по педикюру', 'WB009'),
('Наталья', 'Зайцева', '89990123456', 'Парикмахер', 'WB010'),
('Игорь', 'Григорьев', '89991234567', 'Косметолог', 'WB011'),
('Мария', 'Волкова', '89992345678', 'Визажист', 'WB012'),
('Оксана', 'Яковлева', '89993456789', 'Парикмахер', 'WB013'),
('Татьяна', 'Соколова', '89994567890', 'Массажист', 'WB014'),
('Михаил', 'Кузнецов', '89995678901', 'Маникюрист', 'WB015');

INSERT INTO work_hours (employee_id, work_day, start_time, end_time) VALUES
(1, 'Понедельник', '09:00:00', '17:00:00'),
(2, 'Вторник', '10:00:00', '18:00:00'),
(3, 'Среда', '09:00:00', '17:00:00'),
(4, 'Четверг', '11:00:00', '19:00:00'),
(5, 'Пятница', '08:00:00', '16:00:00'),
(6, 'Суббота', '09:00:00', '17:00:00'),
(7, 'Воскресенье', '10:00:00', '18:00:00'),
(8, 'Понедельник', '09:00:00', '17:00:00'),
(9, 'Вторник', '10:00:00', '18:00:00'),
(10, 'Среда', '09:00:00', '17:00:00'),
(11, 'Четверг', '11:00:00', '19:00:00'),
(12, 'Пятница', '08:00:00', '16:00:00'),
(13, 'Суббота', '09:00:00', '17:00:00'),
(14, 'Воскресенье', '10:00:00', '18:00:00'),
(15, 'Понедельник', '09:00:00', '17:00:00');

INSERT INTO services (id, service_name, description, price, duration) VALUES
(1,'Стрижка', 'Мужская стрижка', 1500.00, 60),
(2,'Окрашивание волос', 'Полное окрашивание волос', 3000.00, 120),
(3,'Маникюр', 'Классический маникюр', 1200.00, 60),
(4,'Педикюр', 'Классический педикюр', 1800.00, 90),
(5,'Массаж', 'Общий массаж всего тела', 2500.00, 90),
(6,'Чистка лица', 'Глубокая чистка кожи лица', 2000.00, 75),
(7,'Макияж', 'Вечерний макияж', 3000.00, 90),
(8,'Наращивание ногтей', 'Акриловое наращивание ногтей', 3500.00, 120),
(9,'Укладка', 'Укладка волос', 1000.00, 45),
(10,'Депиляция', 'Шугаринг', 1500.00, 60),
(11,'Брови и ресницы', 'Коррекция бровей и окрашивание ресниц', 800.00, 45),
(12,'Пилинг', 'Глубокий пилинг кожи', 2500.00, 90),
(13,'Уход за телом', 'Обертывание', 3000.00, 120),
(14,'Стрижка бороды', 'Мужская стрижка бороды', 1000.00, 45),
(15,'Массаж головы', 'Расслабляющий массаж головы', 1200.00, 30);

INSERT INTO appointments (client_id, employee_id, service_id, appointment_date, start_time, end_time, status) VALUES
(1, 1, 1, '2024-10-07 09:00:00', '2024-10-07 09:00:00', '2024-10-07 10:00:00', 'завершено'),
(2, 2, 2, '2024-10-08 10:00:00', '2024-10-08 10:00:00', '2024-10-08 12:00:00', 'завершено'),
(3, 3, 3, '2024-10-09 09:00:00', '2024-10-09 09:00:00', '2024-10-09 10:00:00', 'завершено'),
(4, 4, 4, '2024-10-10 11:00:00', '2024-10-10 11:00:00', '2024-10-10 12:30:00', 'завершено'),
(5, 5, 5, '2024-10-11 08:00:00', '2024-10-11 08:00:00', '2024-10-11 09:30:00', 'завершено'),
(6, 6, 6, '2024-10-12 09:00:00', '2024-10-12 09:00:00', '2024-10-12 10:15:00', 'завершено'),
(7, 7, 7, '2024-10-13 10:00:00', '2024-10-13 10:00:00', '2024-10-13 11:30:00', 'завершено'),
(8, 8, 8, '2024-10-14 09:00:00', '2024-10-14 09:00:00', '2024-10-14 11:00:00', 'завершено'),
(9, 9, 9, '2024-10-15 10:00:00', '2024-10-15 10:00:00', '2024-10-15 10:45:00', 'завершено'),
(10, 10, 10, '2024-10-16 09:00:00', '2024-10-16 09:00:00', '2024-10-16 10:00:00', 'завершено'),
(11, 11, 11, '2024-10-17 11:00:00', '2024-10-17 11:00:00', '2024-10-17 11:45:00', 'завершено'),
(12, 12, 12, '2024-10-18 08:00:00', '2024-10-18 08:00:00', '2024-10-18 09:30:00', 'завершено'),
(13, 13, 13, '2024-10-19 09:00:00', '2024-10-19 09:00:00', '2024-10-19 11:00:00', 'завершено'),
(14, 14, 14, '2024-10-20 10:00:00', '2024-10-20 10:00:00', '2024-10-20 10:45:00', 'завершено'),
(15, 15, 15, '2024-10-21 09:00:00', '2024-10-21 09:00:00', '2024-10-21 09:30:00', 'завершено');