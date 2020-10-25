/**
 * This file is javascript file for the Card Memory Game project. 
 * This file shared between all the *.aspx pages in the project.
 * This script handles the display of instruction, creation of cards, setting and decrementing 
 * timer, checking card similarities and score calculation.
 * 
 * File name     : script.js
 * Project name  : Card Memory Game 
 * Written by    : Goutham
 * Language      : Javascript (ES6)
 * Date modified : 23-09-2020
 * Dependencies  : None
 */

var selectedvalue = 0;    // Used to store the url parameter
var actual_index = 0;     // Number of cards in the game, used as id for each card
var row_selector = 0;     // Number of rows
var column_selector = 0;  // Number of colums
var count_twocards = 0;   // Number of cards flipped
var imgsrc_twocards = []; // Used to store the image sources of the two flipped cards
var id_twocards = [];     // Used to store the id of the two flipped cards
var interval = null;      // ID of the timer used
var correct_flipcount = 0;// Number of correctly identified cards
var flipcount = 0;        // Total number of flips done
var timeleft;             // Number of seconds left
var initial_time;         // Total time given for the game

/**
 * Function that handles timer
 */
function mytimer(){

    if(timeleft == 0){

        // Executes when timer expires
        document.getElementById("time-remain").innerHTML = timeleft;
        clearInterval(interval);
        var game_over = document.getElementById("gameover");
        game_over.classList.add("visible");
    }
    else{

        // Decrease timer each second
        timeleft = timeleft - 1;
        document.getElementById("time-remain").innerHTML = timeleft;
    }
}

/**
 * Function to display instructions at the start of the game
 */
function startinstruction(){

    // Makes instruction visible
    var instruction = document.getElementById("instruction");
    instruction.classList.add("visible");
}

/**
 * Function to create the cards for the game depending on the user selection
 */
function myFunction(){

    // Hide the instruction
    var instruction = document.getElementById("instruction");
    instruction.classList.remove("visible");
    
    // Obtain the selected number from url
    const urlString = window.location.search;
    const urlParams = new URLSearchParams(urlString);
    const selector = urlParams.get("cards");
    selectedvalue = selector;
    console.log(selector);
    
    // Initialize the timer based on the selected value
    switch(selector){
        case "3": timeleft = 30;break;
        case "4": timeleft = 45;break;
        case "5": timeleft = 60;break;
    }
    document.getElementById("time-remain").innerHTML = timeleft;
    initial_time = timeleft;
    interval = setInterval(mytimer,1000);
    
    var grid = document.createElement("div");
    grid.className="grid-container";

    // Initialize number of rows and columns
    if(selector==5){
        row_selector = 4;
        column_selector = selector;
    }   
    else{
        row_selector = selector;
        column_selector = 4; 
    }

    // Get random list of images
    images_list = get_images(selector*2);
          
    for(var row=0;row<row_selector;row++){

        // Create row
        var row_div = document.createElement("div");
        row_div.className="grid-row";
        
        for(var column=0;column<column_selector;column++){

            // Create column and card
            var column_div = document.createElement("div");
            var card_front = document.createElement("div");
            var card_back = document.createElement("div");
            
            column_div.id = actual_index.toString() + "_main";

            // Assign an image to the card
            current_index = Math.floor(Math.random() * images_list.length);
            card_back.style.backgroundImage = 'url(../images/' + images_list[current_index] + ')';
            card_back.style.backgroundSize = "100% 100%";
            
            // remove the assigned image from list
            images_list.splice(current_index,1);
            card_back.classList.add("card","card_back");
            card_back.id = actual_index.toString();
            actual_index += 1;

            // Assign background image to the card
            card_front.style.backgroundImage = "url('../images/card_back_side.jpg')";
            card_front.style.backgroundSize = "100% 100%";
            card_front.classList.add("card","card_front");
            
            // Add event listener to the card to flip them
            column_div.addEventListener("click",flipcard);
            column_div.classList.add("grid-column");

            column_div.appendChild(card_front);
            column_div.appendChild(card_back);
            row_div.appendChild(column_div);
        }
        grid.appendChild(row_div);
    }
    document.getElementById("dummygrid").appendChild(grid);
}


function flipcard() {
    
    // Get the id of the card selected
    var child_id = this.childNodes; 

    // Checks if the card is already selected
    if(!id_twocards.includes(this.id)){

        // Flip the card and increment the flip count
        this.classList.add('is-flipped');
        document.getElementById("filps").innerHTML=++flipcount;
        id_twocards.push(this.id);
        imgsrc_twocards[count_twocards] = child_id[1].style.backgroundImage;
        count_twocards += 1;;

        // When two cards are flipped
        if(count_twocards == 2){
            count_twocards = 0;

            // When the two selected cards are not equal
            if(imgsrc_twocards[0] != imgsrc_twocards[1]){
                document.getElementById("blocker").classList.add("dummygridblocker");

                // Set interval for 700 milliseconds before flipping the cards back
                setTimeout(function(){    
                    
                    // Flip the two cards back and remove from the list
                    document.getElementById(id_twocards[0]).classList.remove('is-flipped');
                    document.getElementById(id_twocards[1]).classList.remove('is-flipped');
                    id_twocards.pop();
                    id_twocards.pop();
                    document.getElementById("blocker").classList.remove("dummygridblocker");
                },700);
            }

            // when the two cards are equal
            else{

                // Remove pointer events for cards and remove from the list
                document.getElementById(id_twocards[0]).style.pointerEvents = "none";
                document.getElementById(id_twocards[1]).style.pointerEvents = "none";
                id_twocards.pop();
                id_twocards.pop();

                // Increment the number of correct flips
                correct_flipcount += 1;

                // Check if all cards are found
                if(correct_flipcount == selectedvalue*2){
                    clearTimeout(interval);
                    var victory = document.getElementById("victory");
                    victory.classList.add("visible");
                    score_calculator();
                }
            }
        }
    }
}

/**
 * This function returns a list of random pairs of images
 * @param {int} selector Number of pair of cards required
 */
function get_images(selector) {

    // List of available images
    var images = ['2_of_clubs.png','2_of_diamonds.png','2_of_hearts.png','2_of_spades.png',
                  '3_of_clubs.png','3_of_diamonds.png','3_of_hearts.png','3_of_spades.png',
                  '4_of_clubs.png','4_of_diamonds.png','4_of_hearts.png','4_of_spades.png',
                  '5_of_clubs.png','5_of_diamonds.png','5_of_hearts.png','5_of_spades.png',
                  '6_of_clubs.png','6_of_diamonds.png','6_of_hearts.png','6_of_spades.png',
                  '7_of_clubs.png','7_of_diamonds.png','7_of_hearts.png','7_of_spades.png',
                  '8_of_clubs.png','8_of_diamonds.png','8_of_hearts.png','8_of_spades.png',
                  '9_of_clubs.png','9_of_diamonds.png','9_of_hearts.png','9_of_spades.png',
                  '10_of_clubs.png','10_of_diamonds.png','10_of_hearts.png','10_of_spades.png',
                  'ace_of_clubs.png','ace_of_diamonds.png','ace_of_hearts.png','ace_of_spades2.png',
                  'jack_of_clubs2.png','jack_of_diamonds2.png','jack_of_hearts2.png',
                  'jack_of_spades2.png','king_of_clubs2.png','king_of_diamonds2.png',
                  'king_of_hearts2.png','king_of_spades2.png','queen_of_clubs2.png',
                  'queen_of_diamonds2.png','queen_of_hearts2.png','queen_of_spades2.png',
                  'red_joker.png'];
    var rand_image = [];
    written = 0;
    while(written < selector){
        var x = Math.floor(Math.random() * images.length);
        
        // Check if image is already in list
        if(!(rand_image.includes(images[x]))){

            // Add images in pair
            rand_image.push(images[x]);
            rand_image.push(images[x]);
            written ++;
        }
      }
      console.log(rand_image);

    return rand_image;
}

/**
 * Function to calculate the score
 * flip score =   Number of cards
 *              -------------------
 *               No. of flips done
 * 
 * time score =  Time left
 *              ------------
 *               Total time
 * 
 * final score = 70% of flip score + 30% of time score
 */
function score_calculator() {

    // Calculate the score
    var flip_score = (1 / flipcount) * (selectedvalue * 4) * 100;
    var time_score = timeleft / initial_time * 100;
    var final_score = parseInt((0.7 * flip_score) + (0.3 * time_score));

    // Display the score
    document.getElementById("UserScore").innerHTML = "Score: " + final_score;
    document.getElementById("UserScoreHidden").value = final_score;
    
}

function display_message(msg) {

    console.log("function");
    alert("This feature is disabled\n" + String(msg));
}
