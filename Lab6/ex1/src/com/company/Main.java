package com.company;

import java.math.BigInteger;
public class Main {

    private static BigInteger NaiveCubicRootSearch(BigInteger a, BigInteger left,BigInteger right)
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
        BigInteger n = new BigInteger("8716664131891073309298060436222387808362956786786341866937428783455365962391673917249574491595229207084297741464557132198229086365652604590297378403184129");
        BigInteger c = new BigInteger("1375865583010982618632308529423371271821438577980922927124130396877925863587827122886875024570556859122064458153631");
        BigInteger e=new BigInteger("3");
        BigInteger m=CubicRoot(c.mod(n));
        BigInteger v= m.modPow(e, n);
        System.out.println(c.equals(v));
    }
}


