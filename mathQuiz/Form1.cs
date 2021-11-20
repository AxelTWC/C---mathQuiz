﻿/* mathQuiz Program 
   Name: Axel Tang
   Date: February 20
   Teacher: Mrs.Schilstra
   Purpose: Creating A Math Quiz program with Windows Form Application C#
*/

/* Changes Made: 
 * Background and design of the Quiz
 * Welcome Text Box when clicking "Start The Quiz" button
 * Colors added to indicate if the player's answer is right or wrong | In addition, I made changes to the subtraction problem where the answer will never be 0 
 * Added a Result at the end of the quiz 
 * Timer's time and its display color
  
 - All Changes made inside Form1.cs will be indicated with a comment, ///<Changes> , this is to find the code and where the exact position of the change is.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Math_Quiz
{

    
    public partial class Form1 : Form
    {
        /*
         All Int here is to declare a variable for future uses
         */

        //Randomizer
        Random randomizer = new Random(); 
        
        //For Addition Problems
        int addend1;
        int addend2;

        //For Subtraction Problems 
        int minuend;
        int subtrahend;

        //For Multiplication Problems
        int multiplicand;
        int multiplier;

        //For Division Problems
        int dividend;
        int divisor;

        //A variable for time in timer
        int timeLeft;

        //Percetage Value for Results
        int percentage = 0;



        //This Constructor is to let us see the design of the form itself [Math Quiz]
        public Form1()
        {
            InitializeComponent();
        }

        //This method is to start the quiz, setting a random number for the questions and making sure the value of NumericUpDown is zero before adding anything to it 
        public void StartTheQuiz()
        {

            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);


            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();


            sum.Value = 0;

            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;


            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);


             ///<Changes>
             //A special feature where subtraction problems will never have an answer of 0 

            if (minuend == subtrahend)
            {
                minuend = minuend + 1;
            }

            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

        }


        //This method is for the Start the Quiz Button 
        private void startButton_Click_1(object sender, EventArgs e)
        {
            
            startButton.Enabled = false;

            ///<Changes>
            //When Button is clicked, the following will show, the reason for using \n is because a textbox will not allow us to create multiple lines
            MessageBox.Show("──────▄▀▀▀▀▀▀▀▄───────\n─────▐─▄█▀▀▀█▄─▌──────\n─────▐─▀█▄▄▄█▀─▌──────\n──────▀▄▄▄▄▄▄▄▀───────\n─────▐▀▄▄▐█▌▄▄▀▌──────\n──────▀▄▄███▄▄▀───────\n█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█\n█░░╦─╦╔╗╦─╔╗╔╗╔╦╗╔╗░░█\n█░░║║║╠─║─║─║║║║║╠─░░█\n█░░╚╩╝╚╝╚╝╚╝╚╝╩─╩╚╝░░█\n█▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█\n\nWelcome To the Math Quiz!\nYou will have 45 seconds to finish the quiz!\nA correct answer will turn green and red if answer is wrong!\nPress OK to start!" , "Welcome!");
            
            //Starts the Quiz
            StartTheQuiz();
            sum.Focus(); //Highlights the sum answer first 
            sum.Select(0, sum.Text.Length);
            
            //Timer starts
            timeLeft = 45; 
            timeLabel.BackColor = Color.Transparent;
            timeLabel.Text = "45 seconds";
            timer1.Start();

        }

        ///<Changes>
        //This method is for the timer's condition 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer()) //If the player got everything right
            {

                timer1.Stop();
                MessageBox.Show("You got all the answers right!\n 100%!",
                                "Congratulations!");
                startButton.Enabled = true;

                //Resets the percentage back to 0 for players who wanted to play again
                percentage = 0;
            }

            else if (timeLeft > 20)  //If the player is still answering but onyl have 20 seconds left
            {

                timeLabel.BackColor = Color.Transparent;
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }

            else if (timeLeft > 10)  //If the player is still answering but onyl have 10 seconds left
            {

                timeLabel.BackColor = Color.Purple;
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds!!";
            }

            else if (timeLeft > 0 && timeLeft <= 10) //If the player has only 10 seconds left, timer goes red
            {
                timeLabel.BackColor = Color.Red;
                timeLeft = timeLeft - 1;
                timeLabel.Text = (timeLeft + " seconds!!!");
            }
            else
            {
                // If the user ran out of time, stop the timer,  it shows a MessageBox, results, and fill in the correct answers afterwards.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.\nYour Results:\n" + percentage + "%", "Oops!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;

                //Resets the percentage back to 0 for players who wanted to play again
                ///<Changes>
                percentage = 0;

            }


        }


        //This method is to check if the player got everything or not. 
        private bool CheckTheAnswer()
        {

                if ((addend1 + addend2 == sum.Value)
           && (minuend - subtrahend == difference.Value)
           && (multiplicand * multiplier == product.Value)
           && (dividend / divisor == quotient.Value))
           return true; //If everything is right, it proceeds to stop timer.
                else
            return false; //If not all answers are correct it does nothing and keeps the program running

        }


        ///<Changes>
        
        /*
         Note: These Methods are manually added and were not from tutorial
         * 
         * All "NumericUpDown's Name".BackColor = Color."Color" are used to set the color if the player got the right answer.
         * All Correct Answer Will Turn Green and Red if its wrong
         * 
         * Percentage = percetnage + 25; Are used to grade how well the player did, adding 25% per each questions.
         */

        //This method is used in the answer for additional problems
        private void sum_ValueChanged(object sender, EventArgs e)
        {
            if (addend1 + addend2 == sum.Value)
            {
                sum.BackColor = Color.Green; 
                percentage = percentage + 25;   

            }
            if (addend1 + addend2 != sum.Value)
            {
                sum.BackColor = Color.Red;
            }
            if (sum.Value == 0)
            {
                sum.BackColor = default(Color);
            }
        }

        //This method is used in the answer for subtraction problems
        private void difference_ValueChanged(object sender, EventArgs e)
        {

            if (minuend - subtrahend == difference.Value)
            {
                difference.BackColor = Color.Green;
                percentage = percentage + 25;
            }
            if (minuend - subtrahend != difference.Value)
            {
                difference.BackColor = Color.Red;
            }
            if (difference.Value == 0)
            {
                difference.BackColor = default(Color);
            }

        }

        //This method is used in the answer for multiplication problems
        private void product_ValueChanged(object sender, EventArgs e)
        {
            if (multiplicand * multiplier == product.Value)
            {
                product.BackColor = Color.Green;
                percentage = percentage + 25;
            }
            if (multiplicand * multiplier != product.Value)
            {
                product.BackColor = Color.Red;
            }
            if (product.Value == 0)
            {
                product.BackColor = default(Color);
            }

        }

        //This method is used in the answer for division problems
        private void quotient_ValueChanged(object sender, EventArgs e)
        {
            if (dividend / divisor == quotient.Value)
            {
                quotient.BackColor = Color.Green;
                percentage = percentage + 25;
            }
            if (dividend / divisor != quotient.Value)
            {
                quotient.BackColor = Color.Red;
            }
            if (quotient.Value == 0)
            {
                quotient.BackColor = default(Color);
            }
        }


        //This method is to highlight the 0 in the answerbox so that the player does not need to manually erase the 0 while type an answer in 
        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);

            }
        }
    }
}
