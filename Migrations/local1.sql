create database beautysalonlocaldb1;

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
('Алиса', 'Смирнова', 'alisa.smirnova@example.com', '+7 (495) 123-4567', 'Стилист'),
('Иван', 'Иванов', 'ivan.ivanov@example.com', '+7 (495) 234-5678', 'Колорист'),
('Екатерина', 'Давыдова', 'ekaterina.davydova@example.com', '+7 (495) 345-6789', 'Мастер маникюра'),
('Давид', 'Бровкин', 'david.brovkin@example.com', '+7 (495) 456-7890', 'Косметолог'),
('Елена', 'Васильева', 'elena.vasileva@example.com', '+7 (495) 567-8901', 'Стилист'),
('Фёдор', 'Мартынов', 'fedor.martynov@example.com', '+7 (495) 678-9012', 'Мастер маникюра'),
('Галина', 'Григорьева', 'galina.grigorieva@example.com', '+7 (495) 789-0123', 'Менеджер'),
('Геннадий', 'Лобанов', 'gennady.lobanov@example.com', '+7 (495) 890-1234', 'Менеджер');

INSERT INTO customers (first_name, last_name, phone, email, birthday) VALUES
('Анна', 'Иванова', '+7 (495) 123-4567', 'anna.ivanova@example.com', '1990-01-01'),
('Борис', 'Петров', '+7 (495) 234-5678', 'boris.petrov@example.com', '1991-02-02'),
('Вера', 'Сидорова', '+7 (495) 345-6789', 'vera.sidorova@example.com', '1992-03-03'),
('Денис', 'Михайлов', '+7 (495) 456-7890', 'denis.mikhailov@example.com', '1993-04-04'),
('Екатерина', 'Кузнецова', '+7 (495) 567-8901', 'ekaterina.kuznetsova@example.com', '1994-05-05'),
('Филипп', 'Козлов', '+7 (495) 678-9012', 'filipp.kozlov@example.com', '1995-06-06'),
('Галина', 'Попова', '+7 (495) 789-0123', 'galina.popova@example.com', '1996-07-07'),
('Георгий', 'Соколов', '+7 (495) 890-1234', 'georgiy.sokolov@example.com', '1997-08-08'),
('Ирина', 'Семенова', '+7 (495) 901-2345', 'irina.semenova@example.com', '1998-09-09'),
('Илья', 'Крылов', '+7 (495) 912-3456', 'ilya.krylov@example.com', '1999-10-10'),
('Лариса', 'Волкова', '+7 (495) 923-4567', 'larisa.volkova@example.com', '2000-11-11'),
('Мария', 'Зайцева', '+7 (495) 934-5678', 'maria.zaitseva@example.com', '2001-12-12'),
('Наталья', 'Лебедева', '+7 (495) 945-6789', 'natalya.lebedeva@example.com', '1995-07-15'),
('Олег', 'Ершов', '+7 (495) 956-7890', 'oleg.ershov@example.com', '1998-08-18'),
('Полина', 'Жукова', '+7 (495) 967-8901', 'polina.zhukova@example.com', '2000-09-19');

INSERT INTO services (title, description, price, duration) VALUES
('Стрижка', 'Обычная стрижка для мужчин и женщин.', 2000.00, 30),
('Окрашивание', 'Полное окрашивание волос.', 7500.00, 90),
('Лечение волос', 'Глубокое кондиционирование и восстановление волос.', 5000.00, 45),
('Укладка', 'Укладка феном и стайлинг.', 3000.00, 60),
('Маникюр', 'Подстригание и полировка ногтей.', 1500.00, 45),
('Педикюр', 'Уход за ногами и полировка ногтей.', 2500.00, 60),
('Чистка лица', 'Глубокая очистка лица.', 5000.00, 60),
('Массаж', 'Расслабляющий массаж всего тела.', 8000.00, 60),
('Депиляция', 'Удаление волос на ногах и руках.', 3500.00, 30),
('Формирование бровей', 'Коррекция и формирование бровей.', 2000.00, 15),
('Макияж', 'Профессиональное нанесение макияжа.', 5000.00, 30),
('Свадебный макияж', 'Специализированный макияж для невест.', 10000.00, 120),
('Кератиновое выпрямление', 'Разглаживающая процедура для непослушных волос.', 15000.00, 120);

INSERT INTO appointments (customer_id, service_id, employee_id, description, date, start_time, end_time, status) VALUES
((SELECT id FROM customers WHERE email = 'anna.ivanova@example.com'), (SELECT id FROM services WHERE title = 'Окрашивание'), (SELECT id FROM employees WHERE email = 'galina.grigorieva@example.com'), 'Запись на окрашивание', '2024-10-16', '2024-10-16 11:00:00', '2024-10-16 12:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'anna.ivanova@example.com'), (SELECT id FROM services WHERE title = 'Чистка лица'), (SELECT id FROM employees WHERE email = 'david.brovkin@example.com'), 'Запись на чистку лица', '2024-10-17', '2024-10-17 14:00:00', '2024-10-17 15:00:00', 'created'),
((SELECT id FROM customers WHERE email = 'anna.ivanova@example.com'), (SELECT id FROM services WHERE title = 'Массаж'), (SELECT id FROM employees WHERE email = 'elena.vasileva@example.com'), 'Запись на массаж', '2024-10-18', '2024-10-18 16:00:00', '2024-10-18 17:00:00', 'success'),
((SELECT id FROM customers WHERE email = 'boris.petrov@example.com'), (SELECT id FROM services WHERE title = 'Педикюр'), (SELECT id FROM employees WHERE email = 'fedor.martynov@example.com'), 'Запись на педикюр', '2024-10-19', '2024-10-19 09:00:00', '2024-10-19 09:45:00', 'success'),
((SELECT id FROM customers WHERE email = 'boris.petrov@example.com'), (SELECT id FROM services WHERE title = 'Лечение волос'), (SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com'), 'Запись на восстановление волос', '2024-10-20', '2024-10-20 10:30:00', '2024-10-20 11:15:00', 'cancelled'),
((SELECT id FROM customers WHERE email = 'boris.petrov@example.com'), (SELECT id FROM services WHERE title = 'Маникюр'), (SELECT id FROM employees WHERE email = 'alisa.smirnova@example.com'), 'Запись на маникюр', '2024-10-21', '2024-10-21 13:00:00', '2024-10-21 13:45:00', 'cancelled'),
((SELECT id FROM customers WHERE email = 'irina.semenova@example.com'), (SELECT id FROM services WHERE title = 'Депиляция'), (SELECT id FROM employees WHERE email = 'ekaterina.davydova@example.com'), 'Запись на депиляцию ног', '2024-10-22', '2024-10-22 15:00:00', '2024-10-22 15:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'irina.semenova@example.com'), (SELECT id FROM services WHERE title = 'Укладка'), (SELECT id FROM employees WHERE email = 'david.brovkin@example.com'), 'Запись на укладку', '2024-10-23', '2024-10-23 14:30:00', '2024-10-23 15:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'boris.petrov@example.com'), (SELECT id FROM services WHERE title = 'Стрижка'), (SELECT id FROM employees WHERE email = 'david.brovkin@example.com'), 'Запись на стрижку', '2024-10-23', '2024-10-23 14:30:00', '2024-10-23 15:30:00', 'success'),
((SELECT id FROM customers WHERE email = 'oleg.ershov@example.com'), (SELECT id FROM services WHERE title = 'Стрижка'), (SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com'), 'Запись на стрижку', '2024-10-24', '2024-10-24 14:30:00', '2024-10-24 15:30:00', 'success'),
((SELECT id FROM customers WHERE email = 'ilya.krylov@example.com'), (SELECT id FROM services WHERE title = 'Стрижка'), (SELECT id FROM employees WHERE email = 'david.brovkin@example.com'), 'Запись на стрижку', '2024-10-25', '2024-10-25 14:30:00', '2024-10-25 15:30:00', 'cancelled'),
((SELECT id FROM customers WHERE email = 'boris.petrov@example.com'), (SELECT id FROM services WHERE title = 'Стрижка'), (SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com'), 'Запись на стрижку', '2024-10-26', '2024-10-26 14:30:00', '2024-10-26 15:30:00', 'success'),
((SELECT id FROM customers WHERE email = 'georgiy.sokolov@example.com'), (SELECT id FROM services WHERE title = 'Стрижка'), (SELECT id FROM employees WHERE email = 'david.brovkin@example.com'), 'Запись на стрижку', '2024-10-23', '2024-10-23 14:30:00', '2024-10-23 15:30:00', 'success'),
((SELECT id FROM customers WHERE email = 'filipp.kozlov@example.com'), (SELECT id FROM services WHERE title = 'Стрижка'), (SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com'), 'Запись на стрижку', '2024-10-26', '2024-10-26 18:30:00', '2024-10-26 19:00:00', 'success');



INSERT INTO schedule (employee_id, date, start_time, end_time) VALUES
((SELECT id FROM employees WHERE email = 'alisa.smirnova@example.com' LIMIT 1), '2024-10-18', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alisa.smirnova@example.com' LIMIT 1), '2024-10-19', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alisa.smirnova@example.com' LIMIT 1), '2024-10-20', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alisa.smirnova@example.com' LIMIT 1), '2024-10-21', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'fedor.martynov@example.com' LIMIT 1), '2024-10-22', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'alisa.smirnova@example.com' LIMIT 1), '2024-10-12', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com' LIMIT 1), '2024-10-19', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com' LIMIT 1), '2024-10-11', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com' LIMIT 1), '2024-10-20', '08:30:00', '16:30:00'),
((SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com' LIMIT 1), '2024-10-22', '08:30:00', '16:30:00'),
((SELECT id FROM employees WHERE email = 'ivan.ivanov@example.com' LIMIT 1), '2024-10-23', '09:00:00', '15:00:00'),
((SELECT id FROM employees WHERE email = 'elena.vasileva@example.com' LIMIT 1), '2024-10-23', '10:00:00', '16:00:00');
