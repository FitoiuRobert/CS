package com.company;
import java.math.BigInteger;

public class Main {

    private static BigInteger NaiveSquareRootSearch(BigInteger a, BigInteger left,BigInteger right)
    {
        // fix root as the arithmetic mean of left and right
        BigInteger root = left.add(right).shiftRight(1);
        // if the root is not between [root, root+1],
        //is not an integer and root is our best integer approximation
        if(!((root.pow(2).compareTo(a) == -1)&&(root.add(BigInteger.ONE).pow(2).compareTo(a) == 1)))
        {
            if (root.pow(2).compareTo(a) == -1)
                root = NaiveSquareRootSearch(a, root,right);
            if (root.pow(2).compareTo(a) == 1)
                root = NaiveSquareRootSearch(a, left,root);
        }
        return root;
    }

    public static BigInteger SquareRoot(BigInteger a)
    {
        return NaiveSquareRootSearch(a, BigInteger.ZERO, a);
    }
    public static void main(String[] args) {
        // TODO code application logic here

        BigInteger n = new BigInteger("837210799");
        BigInteger d = new BigInteger("478341751");
        BigInteger e = new BigInteger("7");
        BigInteger exp = new BigInteger("17");
        BigInteger zero=new BigInteger("0");
        BigInteger unu=new BigInteger("1");
        BigInteger four=new BigInteger("4");
        BigInteger doi=new BigInteger("2");

        BigInteger k , rez, pPlusq, fourMult, delta, x1, x2,sqrRoot;

        k = d.multiply(e).subtract(unu).divide(n).add(unu);//(d*e-1)/n;
        //rez = d.multiply(e).subtract(unu).divide(k);//d*e-1
        pPlusq =n.add(unu).subtract(d.multiply(e).subtract(unu).divide(k)) ;
        //fourMult = n.multiply(four);
        delta =pPlusq.multiply(pPlusq).subtract(n.multiply(four));
        sqrRoot = SquareRoot(delta);

        x1 = pPlusq.add(sqrRoot).divide(doi);
        x2 = pPlusq.subtract(sqrRoot).divide(doi);

        BigInteger verific = x1.multiply(x2);

        System.out.println(n);
        System.out.println(verific);

        System.out.println(exp.modPow(zero.subtract(unu),x1.subtract(unu).multiply(x2.subtract(unu))));
    }
}
