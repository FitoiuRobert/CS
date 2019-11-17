package com.company;

import java.math.BigInteger;

public class Main {

    private static BigInteger NaiveCubicRootSearch(BigInteger a, BigInteger left, BigInteger right)
    {
        // fix root as the arithmetic mean of left and right
        BigInteger root = left.add(right).shiftRight(1);
        // if the root is not between [root, root+1],
        //is not an integer and root is our best integer approximation
        if(!((root.pow(3).compareTo(a) == -1)&&(root.add(BigInteger.ONE).pow(3).compareTo(a) == 1))){
            if (root.pow(3).compareTo(a) == -1)
                root = NaiveCubicRootSearch(a, root, right);
            if (root.pow(3).compareTo(a) == 1)
                root = NaiveCubicRootSearch(a, left, root);
        }
        return root;
    }

    public static BigInteger CubicRoot(BigInteger a)
    {
        return NaiveCubicRootSearch(a, BigInteger.ZERO, a);
    }
    public static void main(String[] args) {
        BigInteger n = new BigInteger("5076313634899413540120536350051034312987619378778911504647420938544746517711031490115528420427319479274407389058253897498557110913160302801741874277608327");
        BigInteger d = new BigInteger("3384209089932942360080357566700689541991746252519274336431613959029831011807259226655786125050887727921274719751986104162037800807641522348207376583379547");
        BigInteger e = new BigInteger("3");

        BigInteger one = new BigInteger("1");
        BigInteger k;
        k = d.multiply(e).subtract(one).divide(n);


    }
}
