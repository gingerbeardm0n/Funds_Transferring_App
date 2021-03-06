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

  SELECT * FROM transfers 
  JOIN accounts on transfers.account_to = accounts.user_id
  JOIN users on accounts.user_id = users.user_id 
  WHERE account_from = 2 OR account_to = 2;

  SELECT u1.username as usernameTo, u2.username as usernameFrom, * FROM transfers 
  JOIN accounts a1 on transfers.account_to = a1.user_id
  JOIN accounts a2 on transfers.account_from = a2.user_id
  JOIN users u1 on a1.user_id = u1.user_id 
  JOIN users u2 on a2.user_id = u2.user_id
  WHERE account_from = 2 OR account_to = 3
  