package com.company;
import java.nio.charset.StandardCharsets;
import java.security.Key;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.Security;
import java.security.SecureRandom;
import javax.crypto.*;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.util.Scanner;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;
import javax.rmi.CORBA.Util;

public class Main {
    public static SecretKey get128BitKeyAES(String s, SecureRandom PRNG) throws NoSuchAlgorithmException, InvalidKeySpecException {
        char[] password = s.toCharArray();
        byte[] salt = new byte[16];
        int iteration_count = 10000;
        int key_size = 128;
//         set salt values to random
        PRNG.nextBytes(salt);
//         initialize key factory for HMAC-SHA1 derivation
        SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA1");
//         set key specification
        PBEKeySpec  pbekSpec =  new PBEKeySpec(password,  salt,  iteration_count, key_size);
//         generate the key
        SecretKey myAESPBKey = new SecretKeySpec( keyFactory.generateSecret(pbekSpec).getEncoded(), "AES");

        return myAESPBKey;
    }

    public static void printAESkey(SecretKey myKey){
        System.out.println("AES key:"+ javax.xml.bind.DatatypeConverter.printHexBinary(myKey.getEncoded()));
    }

    public static void main(String[] args) throws InvalidKeySpecException, NoSuchAlgorithmException, NoSuchPaddingException, InvalidKeyException, ShortBufferException, BadPaddingException, IllegalBlockSizeException {
//        declare securePRNG
        SecureRandom myPRNG = new SecureRandom();
//        get password from user
        Scanner client_input = new Scanner(System.in);
        System.out.println("Enter password: ");
        String raw_password = client_input.nextLine();
        System.out.println("Raw key(password): " + raw_password);
//        build the key from given password
        SecretKey myKey = get128BitKeyAES(raw_password,myPRNG);
        printAESkey(myKey);

//        instantiate AES object
        Cipher myAES = Cipher.getInstance("AES/ECB/PKCS5PADDING");
//        initialize AES object to encrypt mode
        myAES.init(Cipher.ENCRYPT_MODE, myKey);
//        initialize plaintex
        byte[] plaintext = raw_password.getBytes();
//        initialize ciphertext
        byte[] ciphertext = new byte[16];
//        update cipher with the plaintext
        int cLength = myAES.update(plaintext, 0, plaintext.length, ciphertext, 0);
//        process remaining blocks of plaintext
        System.out.println("cLength:" + cLength );
        cLength += myAES.doFinal(ciphertext, cLength);
//        print plaintext and ciphertext
        System.out.println("plaintext: "+ javax.xml.bind.DatatypeConverter.printHexBinary(plaintext));
        System.out.println("ciphertext: "+ javax.xml.bind.DatatypeConverter.printHexBinary(ciphertext));
//        initialize AES for decryption
        myAES.init(Cipher.DECRYPT_MODE, myKey);
//        initialize a new array of bytes to place the decryption
        byte[] dec_plaintext = new byte[16];
        cLength = myAES.update(ciphertext, 0, ciphertext.length, dec_plaintext, 0);
//        process remaining blocks of ciphertex
        cLength += myAES.doFinal(dec_plaintext, cLength);
//        print the new plaintext (hopefully identical to the initial one)
        System.out.println("decrypted: "+ javax.xml.bind.DatatypeConverter.printHexBinary(dec_plaintext));
        System.out.println("decrypted password: "+ new String(dec_plaintext, StandardCharsets.UTF_8));
    }
}
