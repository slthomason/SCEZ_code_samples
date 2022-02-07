<?php

require_once 'scez_client.php';

$client = new ScezClient();

$tokenName = "MyAwesomeNFT";
$tokenDescription = "My Awesome NFT";
$filePath = "logo100.png";

//$client->mintNft($tokenName, $tokenDescription, $filePath);
$nftImage =  $client->uploadNftImage("XPGDn4QZay0LoWwT85ghqqaVeq8/B/PSeWzmWqVxc6A=", $filePath);
echo $nftImage;