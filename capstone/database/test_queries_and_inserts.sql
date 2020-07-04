/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [transfer_id]
      ,[transfer_type_id]
      ,[transfer_status_id]
      ,[account_from]
      ,[account_to]
      ,[amount]
  FROM [tenmo].[dbo].[transfers]

  INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) 
  VALUES (2, 2, 1, 2, 745.12);

  INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) 
  VALUES (2, 2, 1, 3, 1500);

  INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) 
  VALUES (2, 2, 2, 3, 42);

  INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) 
  VALUES (2, 2, 2, 1, 369.54);

  INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) 
  VALUES (2, 2, 3, 1, 45.85);

  INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) 
  VALUES (2, 2, 3, 2, 50.50);

  SELECT * FROM transfers
  WHERE account_from = 2 OR account_to = 2;