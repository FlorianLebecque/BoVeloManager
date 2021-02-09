-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Host: sql2.freemysqlhosting.net
-- Generation Time: Feb 09, 2021 at 11:58 AM
-- Server version: 5.5.54-0ubuntu0.12.04.1
-- PHP Version: 7.0.33-0ubuntu0.16.04.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sql2390507`
--
CREATE DATABASE IF NOT EXISTS `sql2390507` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `sql2390507`;

-- --------------------------------------------------------

--
-- Table structure for table `bv_cat_tKit`
--

CREATE TABLE `bv_cat_tKit` (
  `id_cat` int(10) UNSIGNED NOT NULL,
  `id_tKit` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_cat_tKit`
--

INSERT INTO `bv_cat_tKit` (`id_cat`, `id_tKit`) VALUES
(1, 1),
(1, 3);

-- --------------------------------------------------------

--
-- Table structure for table `bv_catalog`
--

CREATE TABLE `bv_catalog` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_catalog`
--

INSERT INTO `bv_catalog` (`id`, `name`) VALUES
(1, 'City'),
(2, 'Explorer'),
(3, 'All-terrain');

-- --------------------------------------------------------

--
-- Table structure for table `bv_client`
--

CREATE TABLE `bv_client` (
  `id` int(10) UNSIGNED NOT NULL,
  `first_name` varchar(30) NOT NULL,
  `last_name` varchar(30) NOT NULL,
  `enterprise_name` varchar(60) NOT NULL,
  `enterprise_adress` varchar(100) NOT NULL,
  `email` varchar(60) NOT NULL,
  `phone_num` text NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_client`
--

INSERT INTO `bv_client` (`id`, `first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num`, `date`) VALUES
(1, 'Brigitte', 'Macron', 'LesPedales', 'Rue de L\'Elisée n°1 France ', 'SansImportance@gmail.com', '+32 469 69 69', '2021-02-07');

-- --------------------------------------------------------

--
-- Table structure for table `bv_sale`
--

CREATE TABLE `bv_sale` (
  `id` int(10) UNSIGNED NOT NULL,
  `id_client` int(10) UNSIGNED NOT NULL,
  `id_seller` int(10) UNSIGNED NOT NULL,
  `prevision_date` date NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_sale`
--

INSERT INTO `bv_sale` (`id`, `id_client`, `id_seller`, `prevision_date`, `date`) VALUES
(1, 1, 0, '2021-02-17', '2021-02-07');

-- --------------------------------------------------------

--
-- Table structure for table `bv_sale_bike`
--

CREATE TABLE `bv_sale_bike` (
  `id_sale` int(10) UNSIGNED NOT NULL,
  `id_tBike` int(10) UNSIGNED NOT NULL,
  `qnt` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_sale_bike`
--

INSERT INTO `bv_sale_bike` (`id_sale`, `id_tBike`, `qnt`) VALUES
(1, 1, 20),
(1, 2, 5);

-- --------------------------------------------------------

--
-- Table structure for table `bv_tBike_tKit`
--

CREATE TABLE `bv_tBike_tKit` (
  `id_tBike` int(10) UNSIGNED NOT NULL,
  `id_tKit` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_tBike_tKit`
--

INSERT INTO `bv_tBike_tKit` (`id_tBike`, `id_tKit`) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5);

-- --------------------------------------------------------

--
-- Table structure for table `bv_type_bike`
--

CREATE TABLE `bv_type_bike` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` text CHARACTER SET utf8 NOT NULL,
  `price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_type_bike`
--

INSERT INTO `bv_type_bike` (`id`, `name`, `price`) VALUES
(1, 'City', 500),
(2, 'Explorer', 1200);

-- --------------------------------------------------------

--
-- Table structure for table `bv_type_kit`
--

CREATE TABLE `bv_type_kit` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` text CHARACTER SET utf8 NOT NULL,
  `category` int(10) UNSIGNED NOT NULL,
  `properties` text CHARACTER SET utf8
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_type_kit`
--

INSERT INTO `bv_type_kit` (`id`, `name`, `category`, `properties`) VALUES
(1, 'Roue ville', 1, '24'),
(2, 'guidon', 4, NULL),
(3, 'cadre ville', 0, 'rouge'),
(4, 'frein dique', 2, NULL),
(5, 'selle', 3, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `bv_user`
--

CREATE TABLE `bv_user` (
  `id` int(10) UNSIGNED NOT NULL,
  `user` text CHARACTER SET utf8 NOT NULL,
  `psw` text CHARACTER SET utf8 NOT NULL,
  `grade` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bv_user`
--

INSERT INTO `bv_user` (`id`, `user`, `psw`, `grade`) VALUES
(1, 'god', '91ef97b18829119e672b209b831f4b15', 0),
(2, 'florian', '81DC9BDB52D04DC20036DBD8313ED055', 2),
(3, 'Victor', '81DC9BDB52D04DC20036DBD8313ED055', 1),
(5, 'Robin', '22D5D814D801D8B3E1A1C3FC36796DE9', 2),
(6, 'Adrian', '2484E7DAD66B1A2324089BB6C851A97C', 2),
(11, 'John Doe', '81DC9BDB52D04DC20036DBD8313ED055', 1),
(12, 'Bob le bricoleur', '81DC9BDB52D04DC20036DBD8313ED055', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bv_cat_tKit`
--
ALTER TABLE `bv_cat_tKit`
  ADD PRIMARY KEY (`id_cat`,`id_tKit`),
  ADD KEY `id_tKit_kit` (`id_tKit`);

--
-- Indexes for table `bv_catalog`
--
ALTER TABLE `bv_catalog`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `bv_client`
--
ALTER TABLE `bv_client`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `bv_sale`
--
ALTER TABLE `bv_sale`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idClient_client` (`id_client`);

--
-- Indexes for table `bv_sale_bike`
--
ALTER TABLE `bv_sale_bike`
  ADD PRIMARY KEY (`id_sale`,`id_tBike`),
  ADD KEY `id_tBike_tBike` (`id_tBike`);

--
-- Indexes for table `bv_tBike_tKit`
--
ALTER TABLE `bv_tBike_tKit`
  ADD PRIMARY KEY (`id_tBike`,`id_tKit`),
  ADD KEY `idKite_tKit` (`id_tKit`);

--
-- Indexes for table `bv_type_bike`
--
ALTER TABLE `bv_type_bike`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `bike_name` (`name`(30));

--
-- Indexes for table `bv_type_kit`
--
ALTER TABLE `bv_type_kit`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `kit_label` (`name`(20));

--
-- Indexes for table `bv_user`
--
ALTER TABLE `bv_user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bv_catalog`
--
ALTER TABLE `bv_catalog`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `bv_client`
--
ALTER TABLE `bv_client`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `bv_sale`
--
ALTER TABLE `bv_sale`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `bv_type_bike`
--
ALTER TABLE `bv_type_bike`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `bv_type_kit`
--
ALTER TABLE `bv_type_kit`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `bv_user`
--
ALTER TABLE `bv_user`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `bv_cat_tKit`
--
ALTER TABLE `bv_cat_tKit`
  ADD CONSTRAINT `id_tKit_kit` FOREIGN KEY (`id_tKit`) REFERENCES `bv_type_kit` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `id_cat_cat` FOREIGN KEY (`id_cat`) REFERENCES `bv_catalog` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `bv_sale`
--
ALTER TABLE `bv_sale`
  ADD CONSTRAINT `idClient_client` FOREIGN KEY (`id_client`) REFERENCES `bv_client` (`id`);

--
-- Constraints for table `bv_sale_bike`
--
ALTER TABLE `bv_sale_bike`
  ADD CONSTRAINT `id_tBike_tBike` FOREIGN KEY (`id_tBike`) REFERENCES `bv_type_bike` (`id`),
  ADD CONSTRAINT `id_sale_sale` FOREIGN KEY (`id_sale`) REFERENCES `bv_sale` (`id`);

--
-- Constraints for table `bv_tBike_tKit`
--
ALTER TABLE `bv_tBike_tKit`
  ADD CONSTRAINT `idKite_tKit` FOREIGN KEY (`id_tKit`) REFERENCES `bv_type_kit` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `idBike_tBike` FOREIGN KEY (`id_tBike`) REFERENCES `bv_type_bike` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
