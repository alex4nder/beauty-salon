create database beautysalonlocaldb3;
USE beautysalonlocaldb3;

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


-- Добавление сотрудников
INSERT INTO employees (first_name, last_name, email, phone, position) VALUES
('Анна', 'Сидорова', 'anna.sidorova@example.com', '+7 (495) 111-2222', 'Парикмахер'),
('Екатерина', 'Петрова', 'ekaterina.petrova@example.com', '+7 (495) 333-4444', 'Стилист'),
('Дмитрий', 'Смирнов', 'dmitry.smirnov@example.com', '+7 (495) 555-6666', 'Колорист'),
('Ирина', 'Кузнецова', 'irina.kuznetsova@example.com', '+7 (495) 777-8888', 'Массажист'),
('Александр', 'Лебедев', 'alexander.lebedev@example.com', '+7 (495) 999-0000', 'Барбер'),
('Мария', 'Федорова', 'maria.fedorova@example.com', '+7 (495) 123-4567', 'Менеджер'),
('Виктор', 'Алексеев', 'victor.alekseev@example.com', '+7 (495) 234-5678', 'Косметолог');

-- Добавление клиентов
INSERT INTO customers (first_name, last_name, phone, email, birthday) VALUES
('Сергей', 'Афанасьев', '+7 (495) 135-7914', 'sergey.afanasyev@example.com', '1985-02-20'),
('Ольга', 'Рябова', '+7 (495) 246-8025', 'olga.ryabova@example.com', '1991-04-15'),
('Евгений', 'Коновалов', '+7 (495) 357-9136', 'evgeny.konovalov@example.com', '1992-08-05'),
('Анжелика', 'Серова', '+7 (495) 468-0247', 'anzhelika.serova@example.com', '1988-10-30'),
('Николай', 'Борисов', '+7 (495) 579-1358', 'nikolay.borisov@example.com', '1987-12-12'),
('Кристина', 'Маслова', '+7 (495) 681-2469', 'kristina.maslova@example.com', '1990-03-25'),
('Станислав', 'Тихонов', '+7 (495) 792-3580', 'stanislav.tikhonov@example.com', '1989-06-11');

-- Добавление услуг
INSERT INTO services (title, description, price, duration) VALUES
('Уход за лицом', 'Комплексный уход за кожей лица с использованием натуральных средств.', 8000.00, 60),
('Стрижка волос', 'Классическая стрижка с укладкой.', 1500.00, 30),
('Педикюр', 'Классический педикюр с обработкой ногтей и кожи.', 2500.00, 45),
('Маникюр', 'Классический маникюр с покрытием.', 2000.00, 30),
('Окрашивание волос', 'Качественное окрашивание с использованием профессиональных средств.', 6000.00, 90),
('Солярий', 'Загар в солярии за короткое время.', 1000.00, 15),
('Массаж всего тела', 'Расслабляющий массаж всего тела.', 4000.00, 60);

-- Добавление записей на прием
INSERT INTO appointments (customer_id, service_id, employee_id, description, date, start_time, end_time, status) VALUES
((SELECT id FROM customers WHERE email = 'sergey.afanasyev@example.com'), (SELECT id FROM services WHERE title = 'Уход за лицом'), (SELECT id FROM employees WHERE email = 'anna.sidorova@example.com'), 'Запись на уход за лицом', '2024-11-01', '2024-11-01 14:00:00', '2024-11-01 15:00:00', 'success'),
((SELECT id FROM customers WHERE email = 'olga.ryabova@example.com'), (SELECT id FROM services WHERE title = 'Стрижка волос'), (SELECT id FROM employees WHERE email = 'ekaterina.petrova@example.com'), 'Запись на стрижку', '2024-11-02', '2024-11-02 10:00:00', '2024-11-02 10:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'evgeny.konovalov@example.com'), (SELECT id FROM services WHERE title = 'Педикюр'), (SELECT id FROM employees WHERE email = 'dmitry.smirnov@example.com'), 'Запись на педикюр', '2024-11-03', '2024-11-03 12:00:00', '2024-11-03 12:45:00', 'created'),
((SELECT id FROM customers WHERE email = 'anzhelika.serova@example.com'), (SELECT id FROM services WHERE title = 'Маникюр'), (SELECT id FROM employees WHERE email = 'dmitry.smirnov@example.com'), 'Запись на маникюр', '2024-11-04', '2024-11-04 16:00:00', '2024-11-04 16:30:00', 'cancelled'),
((SELECT id FROM customers WHERE email = 'nikolay.borisov@example.com'), (SELECT id FROM services WHERE title = 'Окрашивание волос'), (SELECT id FROM employees WHERE email = 'irina.kuznetsova@example.com'), 'Запись на окрашивание', '2024-11-05', '2024-11-05 11:00:00', '2024-11-05 12:30:00', 'success'),
((SELECT id FROM customers WHERE email = 'kristina.maslova@example.com'), (SELECT id FROM services WHERE title = 'Солярий'), (SELECT id FROM employees WHERE email = 'irina.kuznetsova@example.com'), 'Запись на солярий', '2024-11-06', '2024-11-06 13:00:00', '2024-11-06 13:15:00', 'created'),
((SELECT id FROM customers WHERE email = 'stanislav.tikhonov@example.com'), (SELECT id FROM services WHERE title = 'Массаж всего тела'), (SELECT id FROM employees WHERE email = 'alexander.lebedev@example.com'), 'Запись на массаж', '2024-11-07', '2024-11-07 15:00:00', '2024-11-07 16:00:00', 'created');

-- Добавление расписания
INSERT INTO schedule (employee_id, date, start_time, end_time) VALUES
((SELECT id FROM employees WHERE email = 'anna.sidorova@example.com'), '2024-10-31', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'ekaterina.petrova@example.com'), '2024-11-01', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'dmitry.smirnov@example.com'), '2024-11-02', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'irina.kuznetsova@example.com'), '2024-11-03', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'alexander.lebedev@example.com'), '2024-11-04', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'maria.fedorova@example.com'), '2024-11-05', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'victor.alekseev@example.com'), '2024-11-06', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'anna.sidorova@example.com'), '2024-11-07', '08:00:00', '16:00:00');
