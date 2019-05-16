-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: May 16, 2019 at 11:01 PM
-- Server version: 5.6.38
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `recipe_box`
--
CREATE DATABASE IF NOT EXISTS `recipe_box` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `recipe_box`;

-- --------------------------------------------------------

--
-- Table structure for table `cuisines`
--

CREATE TABLE `cuisines` (
  `id` int(11) NOT NULL,
  `region` varchar(255) NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cuisines`
--

INSERT INTO `cuisines` (`id`, `region`, `description`) VALUES
(1, 'American', 'American cuisine reflects the history of the United States, blending the culinary contributions of various groups of people from around the world, including indigenous American Indians, African Americans, Asians, Europeans, Pacific Islanders, and South Americans. Early Native Americans utilized a number of cooking methods in early American cuisine that have been blended with early European cooking methods to form the basis of what is now American cuisine. The European settlement of the Americas introduced a number of ingredients, spices, herbs, and cooking styles to the continent. The various styles of cuisine continued expanding well into the 19th and 20th centuries, proportional to the influx of immigrants from many different nations; this influx nurtured a rich diversity in food preparation throughout the country.'),
(2, 'Mexican', 'Mexican street food is one of the most varied parts of the cuisine. It can include tacos, quesadillas, pambazos, tamales, huaraches, alambres, al pastor, and food not suitable to cook at home, including barbacoa, carnitas, and since many homes in Mexico do not have or make use of ovens, roasted chicken.');

-- --------------------------------------------------------

--
-- Table structure for table `cuisines_recipes`
--

CREATE TABLE `cuisines_recipes` (
  `id` int(11) NOT NULL,
  `recipe_id` int(11) NOT NULL,
  `cuisine_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cuisines_recipes`
--

INSERT INTO `cuisines_recipes` (`id`, `recipe_id`, `cuisine_id`) VALUES
(1, 1, 0),
(2, 1, 0),
(3, 1, 0),
(4, 1, 1),
(5, 2, 2);

-- --------------------------------------------------------

--
-- Table structure for table `recipes`
--

CREATE TABLE `recipes` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `ingredients` text NOT NULL,
  `instructions` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `recipes`
--

INSERT INTO `recipes` (`id`, `name`, `ingredients`, `instructions`) VALUES
(1, 'Hamburger', '1 1/2 pounds ground beef\r\n2 Tablespoons BBQ sauce\r\n1 teaspoon kosher salt\r\n1/2 teaspoon pepper\r\n1/2 teaspoon garlic powder\r\n4 hamburger buns', 'Combine ground beef, BBQ sauce, salt, garlic powder, and pepper in a medium-sized bowl. Mix just until combined with your hands and shape into 4 patties about 3/4-inch thick. Make a well in your patties with your thumb to prevent from bulging.\r\nPlace burgers on the grill and cook 4 to 5 minutes. Flip and then cook an additional 4-5 minutes, or until juices run clear.\r\nServe hamburgers on buns with your favorite toppings.'),
(2, 'Buffalo Onion Dogs', '3 tablespoons butter\r\n2 large onions, thinly sliced\r\n4 large cloves garlic, chopped\r\nSalt and pepper\r\n1/4 cup hot sauce\r\n8 all-natural hot dogs\r\n8 good-quality hot dog rolls\r\nGrated carrots, finely chopped celery with leafy tops, and pickle relish, for topping\r\n1 cup crumbled smoked blue cheese for topping', 'Preheat a grill to medium-high.\r\n\r\nHeat a large skillet over medium to medium-high. Add the butter. When it melts, add the onions and garlic; season with salt and pepper. Cook, stirring often, until the onions are very soft and deep golden brown, about 20 minutes. Add the hot sauce and reduce the heat to low, adding a splash of water if the onions get too dry.\r\n\r\nGrill the hot dogs, turning occasionally, until dogs are heated through and casings are crispy, about 4 minutes.\r\n\r\nPlace the dogs in rolls; top with the onions, carrots, celery, relish and cheese.');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cuisines`
--
ALTER TABLE `cuisines`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cuisines_recipes`
--
ALTER TABLE `cuisines_recipes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `recipes`
--
ALTER TABLE `recipes`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cuisines`
--
ALTER TABLE `cuisines`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `cuisines_recipes`
--
ALTER TABLE `cuisines_recipes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `recipes`
--
ALTER TABLE `recipes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
