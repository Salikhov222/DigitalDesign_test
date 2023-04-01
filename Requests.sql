/*Сотрудник с максимальной заработной платой*/
SELECT *
FROM Employee
WHERE Salary = (SELECT MAX(Salary) FROM Employee)

/*Максимальная длина цепочки руководителей по таблице сотрудников (вычислить глубину дерева)*/
WITH RECURSIVE ManagerHierarchy (ID, Name, Chief_ID, Depth) AS (
  SELECT ID, Name, Chief_ID, 0
  FROM Employee
  WHERE Chief_ID IS NULL
  UNION ALL
  SELECT e.ID, e.Name, e.Chief_ID, mh.Depth + 1
  FROM Employee e
  JOIN ManagerHierarchy mh ON e.Chief_ID = mh.ID
)
SELECT MAX(Depth)
FROM ManagerHierarchy

/*Отдел с максимальной суммарной зарплатой сотрудников:*/
SELECT d.Name, SUM(e.Salary) AS TotalSalary
FROM Employee e
JOIN Departament d ON e.Departament_ID = d.ID
GROUP BY d.Name
ORDER BY TotalSalary DESC
LIMIT 1

/*Сотрудник, чье имя начинается на «Р» и заканчивается на «н»:*/
SELECT *
FROM Employee
WHERE Name LIKE 'Р%n'
