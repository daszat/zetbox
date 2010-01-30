<?xml version="1.0"?>
<!-- modified to reduce horizontal space on summary report -->
<xsl:stylesheet
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  version="2.0">
  <xsl:output method="text"/>

  <xsl:template match="/">
    <xsl:call-template name="rules">
      <xsl:with-param name="detailnodes" select="//results/rule"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="rules">
    <xsl:param name="detailnodes"/>
    <xsl:for-each select="$detailnodes">
      <xsl:call-template name="defects">
        <xsl:with-param name="detailnodes" select="target/defect"/>
      </xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="defects">
    <xsl:param name="detailnodes"/>
    <xsl:for-each select="$detailnodes">
      <xsl:value-of select="@Source"/>: error :[gendarme] <xsl:value-of select="../../problem"/> Target: <xsl:value-of select="../@Name"/>.
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>