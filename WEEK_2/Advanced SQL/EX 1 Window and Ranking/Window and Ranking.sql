USE RetailDB;
GO

WITH RankedProducts AS (
    SELECT *,
           ROW_NUMBER() OVER (PARTITION BY Category ORDER BY Price DESC) AS Row_Num,
           RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS Rank_Num,
           DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS Dense_Rank_Num
    FROM Products
)
SELECT * FROM RankedProducts WHERE Row_Num <= 3;
