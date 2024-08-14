-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 104.225.208.163    Database: Trivi_Dev
-- ------------------------------------------------------
-- Server version	8.0.37-0ubuntu0.20.04.3

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: Trivi_Dev
--

USE Trivi_Dev;

--
-- Table structure for table `Answers_MC`
--

DROP TABLE IF EXISTS Answers_MC;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Answers_MC (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  question_id varchar(36) NOT NULL,
  answer varchar(40) DEFAULT NULL,
  is_correct tinyint(1) NOT NULL DEFAULT '0',
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY question_id (question_id),
  CONSTRAINT Answers_MC_ibfk_1 FOREIGN KEY (question_id) REFERENCES Questions_MC (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=142 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Collections`
--

DROP TABLE IF EXISTS Collections;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Collections (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  `name` varchar(50) NOT NULL,
  user_id char(36) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY user_id (user_id),
  CONSTRAINT Collections_ibfk_1 FOREIGN KEY (user_id) REFERENCES Users (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Error_Message_Groups`
--

DROP TABLE IF EXISTS Error_Message_Groups;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Error_Message_Groups (
  id smallint unsigned NOT NULL,
  `name` varchar(30) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Error_Messages`
--

DROP TABLE IF EXISTS Error_Messages;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Error_Messages (
  id bigint unsigned NOT NULL,
  group_id smallint unsigned NOT NULL,
  message varchar(250) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id),
  KEY group_id (group_id),
  CONSTRAINT Error_Messages_ibfk_1 FOREIGN KEY (group_id) REFERENCES Error_Message_Groups (id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Game_Question_Status`
--

DROP TABLE IF EXISTS Game_Question_Status;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Game_Question_Status (
  id smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Game_Questions`
--

DROP TABLE IF EXISTS Game_Questions;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Game_Questions (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  question_id varchar(36) NOT NULL,
  game_id varchar(36) NOT NULL,
  game_question_status_id smallint unsigned NOT NULL DEFAULT '1',
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY question_id (question_id,game_id),
  KEY game_id (game_id),
  KEY game_question_status_id (game_question_status_id),
  CONSTRAINT Game_Questions_ibfk_1 FOREIGN KEY (question_id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT Game_Questions_ibfk_2 FOREIGN KEY (game_id) REFERENCES Games (id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT Game_Questions_ibfk_3 FOREIGN KEY (game_question_status_id) REFERENCES Game_Question_Status (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Game_Status`
--

DROP TABLE IF EXISTS Game_Status;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Game_Status (
  id smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Games`
--

DROP TABLE IF EXISTS Games;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Games (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  collection_id char(36) NOT NULL,
  game_status_id smallint unsigned NOT NULL DEFAULT '1',
  randomize_questions tinyint(1) NOT NULL DEFAULT '0',
  question_time_limit smallint unsigned DEFAULT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  started_on timestamp NULL DEFAULT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY collection_id (collection_id),
  KEY game_status_id (game_status_id),
  CONSTRAINT Games_ibfk_1 FOREIGN KEY (collection_id) REFERENCES Collections (id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT Games_ibfk_2 FOREIGN KEY (game_status_id) REFERENCES Game_Status (id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Players`
--

DROP TABLE IF EXISTS Players;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Players (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  game_id varchar(36) NOT NULL,
  nickname varchar(30) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY game_id (game_id),
  CONSTRAINT Players_ibfk_1 FOREIGN KEY (game_id) REFERENCES Games (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Question_Types`
--

DROP TABLE IF EXISTS Question_Types;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Question_Types (
  id smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  prefix char(2) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY id (id),
  UNIQUE KEY `name` (`name`),
  UNIQUE KEY prefix (prefix)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions`
--

DROP TABLE IF EXISTS Questions;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  collection_id char(36) NOT NULL,
  question_type_id smallint unsigned NOT NULL,
  prompt varchar(40) NOT NULL,
  points smallint unsigned NOT NULL DEFAULT '1',
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  KEY collection_id (collection_id),
  KEY question_type_id (question_type_id),
  CONSTRAINT Questions_ibfk_1 FOREIGN KEY (collection_id) REFERENCES Collections (id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT Questions_ibfk_2 FOREIGN KEY (question_type_id) REFERENCES Question_Types (id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=182 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions_MC`
--

DROP TABLE IF EXISTS Questions_MC;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions_MC (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Questions_MC_ibfk_1 FOREIGN KEY (id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions_SA`
--

DROP TABLE IF EXISTS Questions_SA;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions_SA (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  correct_answer varchar(40) NOT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Questions_SA_ibfk_1 FOREIGN KEY (id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Questions_TF`
--

DROP TABLE IF EXISTS Questions_TF;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Questions_TF (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id varchar(36) NOT NULL,
  correct_answer tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Questions_TF_ibfk_1 FOREIGN KEY (id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Responses`
--

DROP TABLE IF EXISTS Responses;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Responses (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  question_id varchar(36) NOT NULL,
  player_id char(36) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  UNIQUE KEY question_id (question_id,player_id),
  KEY player_id (player_id),
  CONSTRAINT Responses_ibfk_1 FOREIGN KEY (question_id) REFERENCES Questions (id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT Responses_ibfk_2 FOREIGN KEY (player_id) REFERENCES Players (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=128 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Responses_MC`
--

DROP TABLE IF EXISTS Responses_MC;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Responses_MC (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  answer_given varchar(36) NOT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Responses_MC_ibfk_1 FOREIGN KEY (id) REFERENCES Responses (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Responses_SA`
--

DROP TABLE IF EXISTS Responses_SA;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Responses_SA (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  answer_given varchar(40) NOT NULL,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Responses_SA_ibfk_1 FOREIGN KEY (id) REFERENCES Responses (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Responses_TF`
--

DROP TABLE IF EXISTS Responses_TF;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Responses_TF (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  answer_given tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  CONSTRAINT Responses_TF_ibfk_1 FOREIGN KEY (id) REFERENCES Responses (id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS Users;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE Users (
  internal_id int unsigned NOT NULL AUTO_INCREMENT /*!80023 INVISIBLE */,
  id char(36) NOT NULL,
  email varchar(200) NOT NULL,
  `password` varchar(250) NOT NULL,
  created_on timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (internal_id),
  UNIQUE KEY internal_id (internal_id),
  UNIQUE KEY id (id),
  UNIQUE KEY email (email)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `View_Answers_MC`
--

DROP TABLE IF EXISTS View_Answers_MC;
/*!50001 DROP VIEW IF EXISTS View_Answers_MC*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Answers_MC` AS SELECT 
 1 AS answer_id,
 1 AS answer_question_id,
 1 AS answer_answer,
 1 AS answer_is_correct,
 1 AS answer_created_on,
 1 AS answer_question_collection_id,
 1 AS answer_question_collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Collections`
--

DROP TABLE IF EXISTS View_Collections;
/*!50001 DROP VIEW IF EXISTS View_Collections*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Collections` AS SELECT 
 1 AS collection_id,
 1 AS collection_name,
 1 AS collection_user_id,
 1 AS collection_created_on*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Error_Messages`
--

DROP TABLE IF EXISTS View_Error_Messages;
/*!50001 DROP VIEW IF EXISTS View_Error_Messages*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Error_Messages` AS SELECT 
 1 AS error_id,
 1 AS group_name,
 1 AS error_group_id,
 1 AS error_message*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Game_Questions`
--

DROP TABLE IF EXISTS View_Game_Questions;
/*!50001 DROP VIEW IF EXISTS View_Game_Questions*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Game_Questions` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS question_created_on,
 1 AS collection_user_id,
 1 AS game_question_game_id,
 1 AS game_question_status_id,
 1 AS game_status_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Games`
--

DROP TABLE IF EXISTS View_Games;
/*!50001 DROP VIEW IF EXISTS View_Games*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Games` AS SELECT 
 1 AS game_id,
 1 AS game_collection_id,
 1 AS game_status_id,
 1 AS game_randomize_questions,
 1 AS game_question_time_limit,
 1 AS game_created_on,
 1 AS game_started_on,
 1 AS game_collection_user_id,
 1 AS active_question_id,
 1 AS next_question_id,
 1 AS count_game_questions,
 1 AS active_question_index*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Players`
--

DROP TABLE IF EXISTS View_Players;
/*!50001 DROP VIEW IF EXISTS View_Players*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Players` AS SELECT 
 1 AS player_id,
 1 AS player_game_id,
 1 AS player_nickname,
 1 AS player_created_on,
 1 AS game_collection_id,
 1 AS game_status_id,
 1 AS game_collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions`
--

DROP TABLE IF EXISTS View_Questions;
/*!50001 DROP VIEW IF EXISTS View_Questions*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS question_created_on,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions_MC`
--

DROP TABLE IF EXISTS View_Questions_MC;
/*!50001 DROP VIEW IF EXISTS View_Questions_MC*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions_MC` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS question_created_on,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions_SA`
--

DROP TABLE IF EXISTS View_Questions_SA;
/*!50001 DROP VIEW IF EXISTS View_Questions_SA*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions_SA` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS question_created_on,
 1 AS collection_user_id,
 1 AS question_correct_answer*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Questions_TF`
--

DROP TABLE IF EXISTS View_Questions_TF;
/*!50001 DROP VIEW IF EXISTS View_Questions_TF*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Questions_TF` AS SELECT 
 1 AS question_id,
 1 AS question_collection_id,
 1 AS question_question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS question_created_on,
 1 AS collection_user_id,
 1 AS question_correct_answer*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Responses`
--

DROP TABLE IF EXISTS View_Responses;
/*!50001 DROP VIEW IF EXISTS View_Responses*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Responses` AS SELECT 
 1 AS response_id,
 1 AS response_created_on,
 1 AS game_id,
 1 AS question_id,
 1 AS question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS player_id,
 1 AS player_nickname,
 1 AS collection_id,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Responses_MC`
--

DROP TABLE IF EXISTS View_Responses_MC;
/*!50001 DROP VIEW IF EXISTS View_Responses_MC*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Responses_MC` AS SELECT 
 1 AS answer_given,
 1 AS response_id,
 1 AS response_created_on,
 1 AS game_id,
 1 AS question_id,
 1 AS question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS player_id,
 1 AS player_nickname,
 1 AS collection_id,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Responses_SA`
--

DROP TABLE IF EXISTS View_Responses_SA;
/*!50001 DROP VIEW IF EXISTS View_Responses_SA*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Responses_SA` AS SELECT 
 1 AS answer_given,
 1 AS correct_answer,
 1 AS response_id,
 1 AS response_created_on,
 1 AS game_id,
 1 AS question_id,
 1 AS question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS player_id,
 1 AS player_nickname,
 1 AS collection_id,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Responses_TF`
--

DROP TABLE IF EXISTS View_Responses_TF;
/*!50001 DROP VIEW IF EXISTS View_Responses_TF*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Responses_TF` AS SELECT 
 1 AS answer_given,
 1 AS correct_answer,
 1 AS response_id,
 1 AS response_created_on,
 1 AS game_id,
 1 AS question_id,
 1 AS question_type_id,
 1 AS question_prompt,
 1 AS question_points,
 1 AS player_id,
 1 AS player_nickname,
 1 AS collection_id,
 1 AS collection_user_id*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Users`
--

DROP TABLE IF EXISTS View_Users;
/*!50001 DROP VIEW IF EXISTS View_Users*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Users` AS SELECT 
 1 AS user_id,
 1 AS user_email,
 1 AS user_password,
 1 AS user_created_on*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'Trivi_Dev'
--

--
-- Dumping routines for database 'Trivi_Dev'
--
/*!50003 DROP PROCEDURE IF EXISTS Activate_Next_Game_Question */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=main@`%` PROCEDURE Activate_Next_Game_Question(
    IN  in_game_id VARCHAR(36)
)
BEGIN
    DECLARE active_qid VARCHAR(36);
    DECLARE next_qid VARCHAR(36);

    -- fetch the active and next question ids from the game
    SELECT
        g.active_question_id,
        g.next_question_id 
    INTO active_qid,
        next_qid
    FROM
        View_Games g
    WHERE
        g.game_id = in_game_id;
    
    -- update the active question's status to closed
    IF active_qid IS NOT NULL THEN
        UPDATE
            Game_Questions
        SET
            game_question_status_id = 3
        WHERE
            game_id = in_game_id
            AND question_id = active_qid;
    END IF;
    
    -- update the next question's status to active
    IF next_qid IS NOT NULL THEN
        UPDATE
            Game_Questions
        SET
            game_question_status_id = 2
        WHERE
            game_id = in_game_id
            AND question_id = next_qid;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS Copy_Game_Questions */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=main@`%` PROCEDURE Copy_Game_Questions(
    IN  in_game_id VARCHAR(36)
)
BEGIN
    DECLARE game_collection_id char(36);
    DECLARE game_status smallint unsigned;
    DECLARE game_randomize_questions bool;
    
    -- fetch the game data
    SELECT 
        g.collection_id, g.game_status_id, g.randomize_questions
    INTO 
        game_collection_id, game_status, game_randomize_questions
    FROM 
        Games g
    WHERE 
        g.id = in_game_id 
    LIMIT 
        1;
    
    -- can't start a game that is not open
    -- IF game_status != 1 then
       -- call Throw_Error_Code(401);
    -- end if;
    
    
    delete from Game_Questions where game_id = in_game_id;
    
    -- copy over the collection questions
    INSERT INTO Game_Questions (question_id, game_id, game_question_status_id)
    (
        SELECT
            q.id,
            in_game_id,
            1
        FROM
            Questions q
        WHERE
            q.collection_id = game_collection_id
        ORDER BY
            IF(game_randomize_questions, rand(), internal_id)   -- if game has elected to randomize questions, order them by radom
    );
    
    -- activate the first game question
	UPDATE
		Game_Questions
	SET
		game_question_status_id = 2
	WHERE
		game_id = in_game_id
	LIMIT
		1;
    
    
    -- return the questions
    SELECT
        q.*
    FROM
        View_Game_Questions q
    WHERE
        q.game_question_game_id = in_game_id;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS Get_Players_Question_Responses */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=main@`%` PROCEDURE Get_Players_Question_Responses(
    IN in_game_id varchar(36),
    IN in_question_id varchar(36)
)
BEGIN

    SELECT
        p.*,
        IF (isnull(r.id), FALSE, TRUE) AS has_response
    FROM
        View_Players p
        LEFT JOIN Responses r ON r.player_id = p.player_id
        AND r.question_id = in_question_id
    WHERE
        p.player_game_id = in_game_id
    GROUP BY
        p.player_id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS Throw_Error_Code */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=main@`%` PROCEDURE Throw_Error_Code(
    IN  error_message_id int unsigned
)
BEGIN
    declare error_string text;
    
    set error_string = convert(error_message_id, char);
    
    SIGNAL SQLSTATE '45000'
    SET MESSAGE_TEXT = error_string;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Current Database: Trivi_Dev
--

USE Trivi_Dev;

--
-- Final view structure for view `View_Answers_MC`
--

/*!50001 DROP VIEW IF EXISTS View_Answers_MC*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Answers_MC AS select a.id AS answer_id,a.question_id AS answer_question_id,a.answer AS answer_answer,a.is_correct AS answer_is_correct,a.created_on AS answer_created_on,q.question_collection_id AS answer_question_collection_id,q.collection_user_id AS answer_question_collection_user_id from (Answers_MC a join View_Questions_MC q on((q.question_id = a.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Collections`
--

/*!50001 DROP VIEW IF EXISTS View_Collections*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Collections AS select c.id AS collection_id,c.`name` AS collection_name,c.user_id AS collection_user_id,c.created_on AS collection_created_on from Collections c */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Error_Messages`
--

/*!50001 DROP VIEW IF EXISTS View_Error_Messages*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Error_Messages AS select e.id AS error_id,g.`name` AS group_name,e.group_id AS error_group_id,e.message AS error_message from (Error_Messages e left join Error_Message_Groups g on((g.id = e.group_id))) order by e.id */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Game_Questions`
--

/*!50001 DROP VIEW IF EXISTS View_Game_Questions*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Game_Questions AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_points AS question_points,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id,gq.game_id AS game_question_game_id,gq.game_question_status_id AS game_question_status_id,g.game_status_id AS game_status_id from ((Game_Questions gq join View_Questions q on((q.question_id = gq.question_id))) join Games g on((g.id = gq.game_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Games`
--

/*!50001 DROP VIEW IF EXISTS View_Games*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Games AS with Game_Questions_Ranked as (select q.question_id AS question_id,q.game_id AS game_id,q.game_question_status_id AS game_question_status_id,q.created_on AS created_on,row_number() OVER (PARTITION BY q.game_id ORDER BY q.game_id )  AS question_index from Game_Questions q) select g.id AS game_id,g.collection_id AS game_collection_id,g.game_status_id AS game_status_id,g.randomize_questions AS game_randomize_questions,g.question_time_limit AS game_question_time_limit,g.created_on AS game_created_on,g.started_on AS game_started_on,c.user_id AS game_collection_user_id,(select q.question_id from Game_Questions_Ranked q where ((q.game_id = g.id) and (q.game_question_status_id = 2)) limit 1) AS active_question_id,(select q.question_id from Game_Questions_Ranked q where ((q.game_id = g.id) and (q.game_question_status_id = 1)) limit 1) AS next_question_id,(select count(0) from Game_Questions_Ranked q where (q.game_id = q.game_id)) AS count_game_questions,(select q.question_index from Game_Questions_Ranked q where ((q.game_id = g.id) and (q.question_id = active_question_id)) limit 1) AS active_question_index from (Games g join Collections c on((c.id = g.collection_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Players`
--

/*!50001 DROP VIEW IF EXISTS View_Players*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Players AS select p.id AS player_id,p.game_id AS player_game_id,p.nickname AS player_nickname,p.created_on AS player_created_on,g.game_collection_id AS game_collection_id,g.game_status_id AS game_status_id,g.game_collection_user_id AS game_collection_user_id from (Players p join View_Games g on((g.game_id = p.game_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions`
--

/*!50001 DROP VIEW IF EXISTS View_Questions*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions AS select q.id AS question_id,c.id AS question_collection_id,q.question_type_id AS question_question_type_id,q.prompt AS question_prompt,q.points AS question_points,q.created_on AS question_created_on,c.user_id AS collection_user_id from (Questions q join Collections c on((c.id = q.collection_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions_MC`
--

/*!50001 DROP VIEW IF EXISTS View_Questions_MC*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions_MC AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_points AS question_points,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id from View_Questions q */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions_SA`
--

/*!50001 DROP VIEW IF EXISTS View_Questions_SA*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions_SA AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_points AS question_points,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id,sa.correct_answer AS question_correct_answer from (View_Questions q join Questions_SA sa on((sa.id = q.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Questions_TF`
--

/*!50001 DROP VIEW IF EXISTS View_Questions_TF*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Questions_TF AS select q.question_id AS question_id,q.question_collection_id AS question_collection_id,q.question_question_type_id AS question_question_type_id,q.question_prompt AS question_prompt,q.question_points AS question_points,q.question_created_on AS question_created_on,q.collection_user_id AS collection_user_id,tf.correct_answer AS question_correct_answer from (View_Questions q join Questions_TF tf on((tf.id = q.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Responses`
--

/*!50001 DROP VIEW IF EXISTS View_Responses*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Responses AS select r.id AS response_id,r.created_on AS response_created_on,p.player_game_id AS game_id,r.question_id AS question_id,q.question_question_type_id AS question_type_id,q.question_prompt AS question_prompt,q.question_points AS question_points,r.player_id AS player_id,p.player_nickname AS player_nickname,q.question_collection_id AS collection_id,q.collection_user_id AS collection_user_id from ((Responses r left join View_Questions q on((q.question_id = r.question_id))) left join View_Players p on((p.player_id = r.player_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Responses_MC`
--

/*!50001 DROP VIEW IF EXISTS View_Responses_MC*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Responses_MC AS select r.answer_given AS answer_given,v.response_id AS response_id,v.response_created_on AS response_created_on,v.game_id AS game_id,v.question_id AS question_id,v.question_type_id AS question_type_id,v.question_prompt AS question_prompt,v.question_points AS question_points,v.player_id AS player_id,v.player_nickname AS player_nickname,v.collection_id AS collection_id,v.collection_user_id AS collection_user_id from (Responses_MC r join View_Responses v on((v.response_id = r.id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Responses_SA`
--

/*!50001 DROP VIEW IF EXISTS View_Responses_SA*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Responses_SA AS select r.answer_given AS answer_given,sa.correct_answer AS correct_answer,v.response_id AS response_id,v.response_created_on AS response_created_on,v.game_id AS game_id,v.question_id AS question_id,v.question_type_id AS question_type_id,v.question_prompt AS question_prompt,v.question_points AS question_points,v.player_id AS player_id,v.player_nickname AS player_nickname,v.collection_id AS collection_id,v.collection_user_id AS collection_user_id from ((Responses_SA r join View_Responses v on((v.response_id = r.id))) join Questions_SA sa on((sa.id = v.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Responses_TF`
--

/*!50001 DROP VIEW IF EXISTS View_Responses_TF*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Responses_TF AS select r.answer_given AS answer_given,tf.correct_answer AS correct_answer,v.response_id AS response_id,v.response_created_on AS response_created_on,v.game_id AS game_id,v.question_id AS question_id,v.question_type_id AS question_type_id,v.question_prompt AS question_prompt,v.question_points AS question_points,v.player_id AS player_id,v.player_nickname AS player_nickname,v.collection_id AS collection_id,v.collection_user_id AS collection_user_id from ((Responses_TF r join View_Responses v on((v.response_id = r.id))) join Questions_TF tf on((tf.id = v.question_id))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Users`
--

/*!50001 DROP VIEW IF EXISTS View_Users*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=main@`%` SQL SECURITY DEFINER */
/*!50001 VIEW View_Users AS select u.id AS user_id,u.email AS user_email,u.`password` AS user_password,u.created_on AS user_created_on from Users u */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-14 15:37:44
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 104.225.208.163    Database: Trivi_Dev
-- ------------------------------------------------------
-- Server version	8.0.37-0ubuntu0.20.04.3

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `Error_Message_Groups`
--
-- ORDER BY:  id

LOCK TABLES Error_Message_Groups WRITE;
/*!40000 ALTER TABLE Error_Message_Groups DISABLE KEYS */;
REPLACE INTO Error_Message_Groups VALUES (1,'Misc'),(2,'Authorization'),(3,'Answers'),(4,'Games'),(5,'Join Game'),(6,'Responses');
/*!40000 ALTER TABLE Error_Message_Groups ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Error_Messages`
--
-- ORDER BY:  id

LOCK TABLES Error_Messages WRITE;
/*!40000 ALTER TABLE Error_Messages DISABLE KEYS */;
REPLACE INTO Error_Messages VALUES (200,2,'Invalid email or password.'),(201,2,'The email you have provided is already associated with an account.'),(202,2,'The passwords do not match.'),(203,2,'Please lengthen the password to 8 or more characters.'),(300,3,'Invalid ID format'),(400,4,'Question time limit must be between 15-60 or null.'),(401,4,'Cannot start a game that is not open.'),(402,4,'You cannot close a question that is already closed.'),(500,5,'Nickname is already taken.'),(501,5,'Could not find a game with matching ID.'),(502,5,'Cannot join a game that has already finished.'),(503,5,'Nickname length must be between 3-30 characters.'),(600,6,'The multiple choice answer is not a valid Answer ID contained in the question\'s options.');
/*!40000 ALTER TABLE Error_Messages ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Question_Types`
--
-- ORDER BY:  id

LOCK TABLES Question_Types WRITE;
/*!40000 ALTER TABLE Question_Types DISABLE KEYS */;
REPLACE INTO Question_Types VALUES (1,'Multiple Choice','mc'),(2,'True False','tf'),(3,'Short Answer','sa');
/*!40000 ALTER TABLE Question_Types ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Game_Status`
--
-- ORDER BY:  id

LOCK TABLES Game_Status WRITE;
/*!40000 ALTER TABLE Game_Status DISABLE KEYS */;
REPLACE INTO Game_Status VALUES (1,'Open'),(2,'Running'),(3,'Completed'),(4,'Disconnected');
/*!40000 ALTER TABLE Game_Status ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Game_Question_Status`
--
-- ORDER BY:  id

LOCK TABLES Game_Question_Status WRITE;
/*!40000 ALTER TABLE Game_Question_Status DISABLE KEYS */;
REPLACE INTO Game_Question_Status VALUES (1,'Pending'),(2,'Active'),(3,'Closed');
/*!40000 ALTER TABLE Game_Question_Status ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-14 15:37:50
